<?php
	$fp = fopen('Player2.data', 'r');
	$i = 0;
	while( ($data = fgets($fp)) !== false){
		print $data;
	}
	fclose($fp);
