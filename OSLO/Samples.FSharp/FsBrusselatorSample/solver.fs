module solver

open System.Linq
open System.Windows;
open Microsoft.Research.Oslo



let ode_solve2d in_Time in_x1 in_x2 (rightPart:float->float->float->float*float) end_Time step  =    
    let rp t (v:Vector) =        // defines right part so that it can be passed as a .NET delegate to the RK547 method
        let (x1, x2) = rightPart t v.[0] v.[1]
        Vector(x1,x2)
    let rk = Ode.RK547M (in_Time, // initial time point
                        Vector(in_x1, in_x2),  // initial X vector
                        System.Func<double,Vector,Vector>(rp)) // right part
    rk.SolveFromToStep(in_Time, end_Time, step).ToArray()    
                         

let times (solution:SolPoint[]) =
    Array.map (fun (q:SolPoint) -> q.T) solution 

let data n (solution:SolPoint[]) =
    Array.map (fun (q:SolPoint) -> q.X.[n]) solution 