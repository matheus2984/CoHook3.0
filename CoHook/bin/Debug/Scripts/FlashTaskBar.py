import clr
clr.AddReference("COAPI")
from COAPI import *
from System import *

def Execute(conquer):
	address = 0x40B465
	byteValue = conquer.Memory.ReadByte(address)
	if byteValue == 0x75:
		newByteValue = 0xEB
	else:
		newByteValue = 0x75
	conquer.Memory.Write(address, newByteValue)