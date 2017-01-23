<?php
	$fp = fopen('Player1.data', 'w');
	fwrite($fp, $_GET['class']);
	fclose($fp);

echo $_GET['class'];
