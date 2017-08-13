
				set NET="%PROGRAMFILES%\Microsoft.NET\SDK\v1.1\Bin"
				%NET%\gacutil /if IKVM.Runtime.dll
				%NET%\gacutil /if IKVM.GNU.Classpath.dll
				%NET%\gacutil /if saxon8.dll 
				%NET%\gacutil /if saxon8api.dll                
      