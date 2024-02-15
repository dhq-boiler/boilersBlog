using Cupy;
using Python.Runtime;

Runtime.PythonDLL = @"C:\Users\boiler\AppData\Local\Programs\Python\Python311\python311.dll";
PythonEngine.Initialize();

// before starting the measurement, let us call CuPy once to get the setup checks done. 
cp.arange(1);

var a1 = cp.arange(60000).reshape(300, 200);
var a2 = cp.arange(80000).reshape(200, 400);

var result = cp.matmul(a1, a2);

Console.WriteLine(result.repr);

