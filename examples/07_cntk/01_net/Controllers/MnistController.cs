﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CNTK;
using System.IO;
using System.Web.Hosting;

namespace _01_net.Controllers
{
  // Notes: x64, start code, saving model
  public class MnistController : ApiController
  {
    private static readonly float[] Image = new float[]
    {
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 31, 132, 254,
      253, 254, 213, 82, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 21, 142, 233,
      252, 253, 252, 253, 252, 223, 20, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 123, 254,
      253, 254, 253, 224, 203, 203, 223, 255, 213, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      203, 253, 252, 253, 212, 20, 0, 0, 61, 253, 252, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 41, 243, 224, 203, 183, 41, 152, 30, 0, 0, 255, 253,
      102, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 40, 20, 0, 0, 102, 253, 50, 0, 82,
      253, 252, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 82, 214, 31,
      113, 233, 254, 233, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 62, 102, 82, 41,
      253, 232, 253, 252, 233, 50, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 152, 253,
      254, 253, 254, 253, 254, 233, 123, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      152, 252, 253, 252, 253, 252, 192, 50, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 62, 183, 203, 243, 254, 253, 62, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 40, 172, 252, 203, 20, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 21, 0, 0, 0, 0, 0, 0, 0, 0, 0, 183, 254,
      112, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 62, 203, 163, 0, 0, 0, 0, 0, 0, 0, 0,
      61, 253, 151, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 21, 214, 192, 0, 0, 0, 0, 0, 0, 0,
      0, 11, 213, 254, 151, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 102, 253, 151, 0, 0, 0, 0, 0,
      0, 0, 41, 213, 252, 253, 111, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 41, 255, 213, 92, 51, 0,
      0, 31, 92, 173, 253, 254, 253, 142, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 172, 252, 253,
      252, 203, 203, 233, 252, 253, 252, 253, 130, 20, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 21,
      203, 255, 253, 254, 253, 254, 253, 244, 203, 82, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 20, 151, 151, 253, 171, 151, 151, 40, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0
    };

    [HttpGet]
    public int Predict()
    {
      var p = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, ".\\mnist");
      var model = Function.Load(p, DeviceDescriptor.CPUDevice);

      var inputVariable = model.Arguments.First();
      var inputShape = inputVariable.Shape;

      var inputDataMap = new Dictionary<Variable, Value>();
      var inputValue = Value.CreateBatch(inputShape, Image, DeviceDescriptor.CPUDevice);
      inputDataMap.Add(inputVariable, inputValue);

      var outputVariable = model.Output;
      var outputDataMap = new Dictionary<Variable, Value>();
      outputDataMap.Add(outputVariable, null);

      model.Evaluate(inputDataMap, outputDataMap, DeviceDescriptor.CPUDevice);

      var outputValue = outputDataMap[outputVariable];
      var outputData = outputValue.GetDenseData<float>(outputVariable).First();

      Tuple<int, float> max = null;
      for (var i = 0; i < outputData.Count; i++)
      {
        var current = outputData[i];
        if (max == null || max.Item2 < current)
        {
          max = new Tuple<int, float>(i, current);
        }
      }

      return max.Item1;
    }
  }
}
