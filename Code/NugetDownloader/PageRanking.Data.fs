module PageRanking.Data

open FSharp.Data
open System.IO
open System.IO.Compression
open MBrace.FsPickler
open Paket


type AutoComplete =
    JsonProvider< """{"@context":{"@vocab":"http://schema.nuget.org/schema#"},"totalHits":59717,"lastReopen":"2016-10-05T12:05:37.4829982Z","index":"v3-lucene0-v2v3-20161003","data":["ABCFramework.Wpf","44nwodhterag"]}""" >

let NuGetV2URL = "https://www.nuget.org/api/v2"
let NuGetV3URL = "https://api.nuget.org/v3/index.json"
let BatchSize = 1000
let ModelFile = __SOURCE_DIRECTORY__ + "/nuget-latest-versions.model"

let fetchPackageIds () =

    let fetchingUrl =
        NuGetV3.getSearchAPI(None, NuGetV3URL)
        |> Async.AwaitTask
        |> Async.RunSynchronously

    let rec getPackages url skip =
      seq {
        let response =
            sprintf "%s?skip=%d&take=%d"
                url skip BatchSize
            |> AutoComplete.Load
        yield! response.Data
        if response.Data.Length = BatchSize
        then yield! getPackages url (skip+BatchSize)
      }
    match fetchingUrl with
    | Some(url) -> getPackages url 0
    | _ -> Seq.empty

let downloadPackageMetadata packageId =
  async {
    let packageData = Domain.PackageName(packageId)
    let! versioning =
        NuGetV2.tryGetPackageVersionsViaJson(None, NuGetV2URL, packageData)
        |> Async.Catch
    match versioning with
    | Choice1Of2(Some(versions)) when versions.Length > 0 ->
        let latest = SemVer.Parse(versions.[versions.Length-1])
        let! metadata =
            NuGetV2.getDetailsFromNuGetViaODataFast None NuGetV2URL packageData latest
            |> Async.Catch
        return
            match metadata with
            | Choice1Of2(data) -> Some(data)
            | _ -> None
    | _ -> return None
  }

let downloadAllPackagesFromNuget () =
    fetchPackageIds()
    |> Seq.map downloadPackageMetadata
    |> Async.Parallel
    |> Async.RunSynchronously
    |> Array.choose (id)


let downloadPackageData () =
    let binarySerializer = FsPickler.CreateBinarySerializer()

    if File.Exists ModelFile then
        binarySerializer.DeserializeSequence<NuGet.NuGetPackageCache>(new GZipStream(new FileStream(ModelFile, FileMode.Open), CompressionMode.Decompress))
        |> Seq.toArray
    else
        let model = downloadAllPackagesFromNuget()     
        binarySerializer.SerializeSequence (new GZipStream(new FileStream(ModelFile, FileMode.Create), CompressionLevel.Optimal), model) 
        |> ignore 
        model