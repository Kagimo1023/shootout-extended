{ pkgs }: {
	deps = [
  pkgs.msbuild
  pkgs.dotnet-sdk
    pkgs.omnisharp-roslyn
	];
}