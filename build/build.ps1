Param(
	[parameter(Mandatory=$false)][string]$num="*"
)
#$basePath=".."
$dockerFiles=@(
@{name='apisms';path='API.SMS/Dockerfile'}
@{name='smsbackgroundtask';path='SMSBackgroundTask/Dockerfile'}

)
if($num -eq "*"){
$basePath=".."
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
		docker build -f $path --force-rm -t ${name}:latest . #$basePath	
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
