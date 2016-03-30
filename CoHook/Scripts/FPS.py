import clr
clr.AddReference("COAPI")
from COAPI import *
from System import *

def Execute(conquer):
	address = 0x601419
	byteValue = conquer.Memory.ReadByte(address)
	if byteValue == 0x19:
		newByteValue = 0x5
	else:
		newByteValue = 0x19
	conquer.Memory.Write(address, newByteValue)