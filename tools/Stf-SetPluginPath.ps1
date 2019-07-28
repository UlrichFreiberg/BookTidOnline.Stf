param ($PluginFolderType)

###################################################
# Set a plugin folder
###################################################
function Stf-SetPluginFolder()
{
    param (
        [parameter(Mandatory=$true)]
        [ValidateSet("build", "temp", "tempstf")]
        [string] $PluginFolderType,
        [string] $ConfigDir = "c:\temp\Stf\Config\"
    )

    $stfRoot = (Resolve-Path "$PSScriptRoot\..").Path
	$buildOutput = (Resolve-Path "$stfRoot\StfBin").Path
	$stfTempBin = "C:\Temp\Stf\StfBin"
					
	$pluginFolderPath = [string]::Empty
	
	switch ($PluginFolderType) {
		"temp" {
			$pluginFolderPath = $stfTempBin             
			break
		}
		"tempstf" {
			$pluginFolderPath = $stfTempBin                                             
			break
		}
		"build" {
			$pluginFolderPath = $buildOutput
			break
		}
		default {
			$pluginFolderPath = $stfTempBin
			break
		}
	}
	
	$configFiles = Get-Item (Join-Path (Resolve-Path $ConfigDir) "StfConfiguration*.xml")
    Write-Host

	foreach ($configFile in $configFiles) {					
		$xmlConfig = [xml](Get-Content $configFile)
		$stfKernel = $xmlConfig.configuration.section | Where-Object { $_.name -eq "StfKernel" }
		$pluginPath = $stfKernel.key | Where-Object { $_.name -eq "PluginPath" }

        if ($pluginPath -ne $null) {
    		Write-Host ("Setting PluginPath to [{0}] in [{1}]" -f $pluginFolderPath, $configFile)

    		$pluginPath.value = $pluginFolderPath
            $xmlConfig.Save($configFile)
        }
	}
}


##################################################
#
# M A I N
#
##################################################
Stf-SetPluginFolder $PluginFolderType
