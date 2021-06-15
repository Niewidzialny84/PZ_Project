<?php

session_start();

	if ((!isset($_POST['login'])) || (!isset($_POST['passwd'])))
	{
		header('Location: index.php');
		exit();
	}

	$login = $_POST['login'];
	$passwd = $_POST['passwd'];

  require_once("headerParser.php");

  Protocol::begin();
  $out = Protocol::login($login,$passwd);

if($out != -1) {
  $_SESSION['logged'] = true;
  
  $_SESSION['login'] = $login;

  unset($_SESSION['errr']);
  header('Location: main.php');
} else {
  Protocol::disconnect("Invalid credentials");
  $_SESSION['errr'] = '<span style="color:red">Invalid login or password!</span>';
  header('Location: index.php');
}

?>