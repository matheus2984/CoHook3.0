import clr
clr.AddReference("COAPI")
from COAPI import *
from System import *

def Execute(conquer):
	Console.WriteLine(conquer.Memory.ReadString(0xA0B3BC,16))