Param(
	[parameter(Mandatory=$false)][string]$num="*"
)
#$basePath=".."
$dockerFiles=@(
@{name='apivts';path='/Api.VTS/Dockerfile'}
@{name='websharelink';path='/Services/ShareLink/Web.ShareLink/Dockerfile'}
@{name='apigeofence';path='/Services/Geofence/API.Geofence/Dockerfile'}
@{name='apinotifier';path='/Services/NotifierService/API.Notifier/Dockerfile'}
@{name='apireports';path='/Services/Reports/API.Reports/Dockerfile'}

)
if($num -eq "*"){
For($i=0;$i -lt $dockerFiles.Count;$i++){
	$name=$dockerFiles[$i].name;
	$path=$dockerFiles[$i].path;
	echo "$i) [$name] - [$path] " 
}

echo ""
$num = read-host "Select number(s) seperated by space"
}
echo "INPUT: [$num]"
$indexes=$num.Split(",");
$indexes


For($i=0;$i -lt $dockerFiles.Count;$i++){
	$name=$dockerFiles[$i].name;
	$path=$dockerFiles[$i].path;
	
	if($num -eq ""){
		echo  "BUILDING $name  $path";
		#echo "-f $basePath$path --force-rm -t ${name}:latest";
		docker build -f $basePath$path --force-rm -t ${name}:latest . #$basePath
	}
	elseif( $i -in $indexes){
		$path="$($path).develop"; 
		echo  "BUILDING $name  $path";
		#echo "-f $basePath$path --force-rm -t ${name}:latest";
		docker build -f $path --force-rm -t ${name}:latest .#$basePath	
	}
	elseif(-not($i -in $indexes)){
		
	}
	
}

# ForEach($file in $dockerFiles){
#	$df=$dockerFiles[$answer]
#	if(!$df){
#		echo "WRONG FILE INDEX $answer"
#		continue;
#	}
		
#	echo  "BUILDING $df"
#	docker build -f "$basePath$df" --force-rm -t tv2apisaasadministration:latest $basePath   
#}

echo ""
Write-Host "BUILD COMPLETED"
