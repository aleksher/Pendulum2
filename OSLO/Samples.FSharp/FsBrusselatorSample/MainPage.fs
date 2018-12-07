// Sample: solving an ordinary differential equation (ODE) "Brusselator model" using the Oslo library,
// and visualizing results using D3 for Silverlight library (http://dynamicdatadispaly.codeplex.com).
// 
// The equation is 
// dx1/dt = 1 + x1*x1*x2 - 4*x1
// dx2/dt = 3*x1 - x1*x1*x2
// x1(0) = 1.01, x2(0) = 3.0,
// 0 <= t <= 20.0
//
// The solver.fs file contains a helper function "ode_solve2d" which wraps an Ode.RK547M method,
// exposed by the Oslo library, added to the References.
namespace FSharpSilverlightApp

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Media.Imaging
open System.Windows.Media
open Microsoft.Research.DynamicDataDisplay
open solver


type MainPage() as this = 
    inherit UserControl()
    do
        // (1) Preparing UI elements to draw the solution:
        // the window contains Grid, which contains Chart, a control of the D3 library.
        // The Chart is a control that renders a figure with axes and grid lines, and enables navigation.
        // We put another Grid into the Chart as a container for two LineGraphs (of the D3 library),
        // so that they will represent computed x1 and x2 data.
        let grid = Grid()
        this.Content <- grid
        let chart = Chart(Title="Brusselator Sample")
        grid.Children.Add(chart)
        let chartGrid = Grid() // chart grid is a place within chart to place the graphs
        chart.Content <- chartGrid

        // (2) Solving the ODE.
        // "brusselator" is a function of the right part of the ODE,
        // "ode_solve2d" is a function defined in the solver.fs and it uses Ode.RK547M method of the Oslo library
        //               to solve the equation, and returns an array of SolPoint elements.
        let brusselator t x1 x2 = (1.0+x1*x1*x2-4.0*x1, 3.0*x1-x1*x1*x2) // right part of the ODE
        let solution = ode_solve2d 0.0 1.01 3.0 brusselator 20.0 0.01 // initial time and x1,x2; right part; end time; step.

        // (3) Visualizing the solution.
        // "plot" function takes x and y coordinates and adds a LineGraph to the Chart's grid,
        //        and then plots a polyline for the data.
        let plot (x:float[]) (y:float[]) title stroke = 
            do    
                let gr = LineGraph(
                            Description=title,
                            Stroke=SolidColorBrush(stroke),
                            StrokeThickness=2.0
                            )          
                chartGrid.Children.Add(gr);
                gr.Plot(x, y)

        // "times" and "data" are functions defined in the solver.fs
        // "times" produces from the solution array an array with time grid data
        // "data" produces an array from the solution, containg given component; "data 0" is "x1", "data 1" is "x2"
        plot (times solution) (data 0 solution) "x1" Colors.Orange
        plot (times solution) (data 1 solution) "x2" Colors.Blue



