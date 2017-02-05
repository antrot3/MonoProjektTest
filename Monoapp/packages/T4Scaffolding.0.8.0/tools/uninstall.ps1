param($rootPath, $toolsPath, $package, $project)

# Try to delete the solution-level config file, if there is one
if ($project) {
	$solutionDir = [System.IO.Path]::GetDirectoryName($project.DTE.Solution.FullName)
	$configFile = Join-Path $solutionDir "scaffolding.config"
	if (Test-Path $configFile) {
		Set-IsCheckedOut $configFile
		del $configFile
	}
}