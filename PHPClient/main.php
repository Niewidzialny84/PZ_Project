<?php
  session_start();
	require_once("loginstate.php");
?>

<html>
  <head>
    <title>Test</title>
    <link rel="stylesheet" href="styles.css">
  </head>
  <body>
  <?php

	echo "<p>Hi ".$_SESSION['login'].'!</p>';

  echo '<form method="post" action="logout.php">
          <input type="submit" value="Logout" />
        </form>';

  echo '<form method="post" action="categories.php">
          <input type="submit" value="Quiz" />
        </form>';

  echo '<form method="post" action="stats.php">
          <input type="submit" value="Stats" />
        </form>';   
  ?>
  </body>
</html>