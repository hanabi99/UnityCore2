﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GeneratorType
{
    Find,//组件查找
    Bind,//组件绑定
}
public enum ParseType
{
    Name,
    Tag
}
public class GeneratorConfig
{
    public static string BindComponentGeneratorPath = Application.dataPath + "/Scripts/ZMUIFrame/BindCompoent";
    public static string FindComponentGeneratorPath = Application.dataPath + "/Scripts/ZMUIFrame/FindCompoent";
    public static string WindowGeneratorPath = Application.dataPath + "/Scripts/ZMUIFrame/Window";
    public static string OBJDATALIST_KEY = "objDataList";
    public static GeneratorType GeneratorType = GeneratorType.Bind;
    public static ParseType ParseType = ParseType.Name;
    public static string[] TAGArr = { "Image", "RawImage", "Text", "Button", "Slider", "Dropdown", "InputField", "Canvas", "Panel", "ScrollRect" ,"Toggle"};
}
