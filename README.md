# LZFSESharp
 LZFSESharp is a .NET port of the lzfse compression algorithm which is being developed by Apple.

## Compatibility
The LZFSESharp project builds to .NET Framework 4.6.2.

Durning compilation, the lzfse.dll file will be moved to the build directory.

## Usage:
* ### Compression

```csharp
byte[] compressedBuffer = LZFSE.Compress(rawInputBuffer);
```

You will need to store the size of the Raw Uncompressed buffer in order for decompression to be optimal.

***

* ### Decompression

```csharp
byte[] uncompressedBuffer = LZFSE.Decompress(compressedBuffer, rawSize [optional], decompressionSizeFactor [optional]);
```

If the method is provided a Raw Size then the inital size of the output buffer will be `rawSize` resulting in less memory usage.

If it is not provided a Raw Size, the initial size of the output buffer will be `compressedBuffer.Length * decompressionSizeFactor`. This results in very high memory usage as we can allocate more memory than needed.
