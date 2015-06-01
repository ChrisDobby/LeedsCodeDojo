#r "references/OpenTK.dll"
#r "references/OpenTK.GLControl.dll"
#r "references/FSharp.Data.dll"
#load "functional3d.fs"
#load "TryFsharpUtilities.fs"

open Functional3D
open TryFsharpUtilities
open System.Drawing
open FSharp.Data

type Shape =
    | Cuboid of float * float * float
    | Cube of float
    | Cone of float * float
    | Cylinder of float * float

type ShapeDef =
    {
        Shape : Shape
        Coords : (float * float * float) option
        Colour : Color
    }

let draw shapeDefs coords = 
    let getDrawing shapeDef coords =
        let x, y, z = shapeDef.Coords |> coords
        match shapeDef.Shape with
            | Cuboid (width, height, depth) -> TryFs.cuboid x y z width height depth shapeDef.Colour
            | Cube height -> TryFs.cube x y z height shapeDef.Colour
            | Cone (width, height) -> TryFs.cone x y z height width shapeDef.Colour
            | Cylinder (width, height) -> TryFs.cylinder x y z 3. 1. shapeDef.Colour

    shapeDefs |> List.map(fun s -> getDrawing s coords) |> TryFs.showEm

let defaultCoords c = match c with | None -> (0., 0., 0.) | Some(xyz) -> xyz
let moveDiagonal c = let x, y, z = c
                     (x - 0.5, y - 0.5, z)

draw [{Shape = Cylinder (3., 1.); Coords = None; Colour = Color.AliceBlue}
      {Shape = Cube 2.; Coords = Some(1., 1., 0.); Colour = Color.Gold}] 
     (defaultCoords >> moveDiagonal)
