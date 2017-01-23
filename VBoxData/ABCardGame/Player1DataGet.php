<?php
	$fp = fopen('Player1.data', 'r');
	$i = 0;
	while( ($data = fgets($fp)) !== false){
		echo $data;
	}
	fclose($fp);
