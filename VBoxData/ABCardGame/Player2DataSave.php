<?php
	$fp = fopen('Player2.data', 'w');
	fwrite($fp, $_GET['class']);
	fclose($fp);

echo $_GET['class'];
