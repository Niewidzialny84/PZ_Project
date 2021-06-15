<?php

	session_start();
	
  require_once("headerParser.php");
  Protocol::disconnect("Bye");

	session_unset();
	
	header("Location: index.php");

?>