//DO NOT Change the name of this namespace!
//Visual Studio will fail to recognise the new namespace and you'll break stuff!
namespace SilverlightApp

open System.Windows
open System.Windows.Controls

type App() as this = 
    inherit Application()
    do
        //Remember to change the namespace on Main page here if you change it in MainPage.fs
        this.Startup.AddHandler(fun o e -> this.RootVisual <- FSharpSilverlightApp.MainPage())
