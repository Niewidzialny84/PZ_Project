<?php
  session_start();
	require_once("loginstate.php");
  require_once("headerParser.php");
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

  if(isset($_SESSION['iter']) && isset($_POST['answ'])) {
    if($_POST['answ'] == $_SESSION['corr']) {
      $_SESSION['score'] += 10;
    } else {
      $_SESSION['score'] += 1;
    }

    echo '<p>Your score is: '.$_SESSION['score'].'</p>';

    Protocol::endQuiz($_SESSION['score']);
    unset($_SESSION['iter']);
    unset($_SESSION['score']);
  }
  
  echo '<form method="post" action="main.php">
          <input type="submit" name="ret" value="Return" />
        </form>';   
  ?>
  </body>
</html>