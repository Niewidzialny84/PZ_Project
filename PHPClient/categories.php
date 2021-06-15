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
  if(isset($_POST['ret'])) {
    Protocol::endQuiz(0);
    unset($_SESSION['iter']);
    unset($_SESSION['score']);
  }

	echo "<p>Hi ".$_SESSION['login'].'!</p>';

  echo '<form method="post" action="logout.php">
          <input type="submit" value="Logout" />
        </form>';

  $cat = Protocol::getCategories();

  echo '<form method="post" action="quiz.php">';
  foreach($cat as $x => $val) {
    echo '<input type="submit" name="cat" value="'.$val.'" /></br></br>';
  }
  echo '</form>';
  
  echo '<form method="post" action="main.php">
          <input type="submit" value="Return" />
        </form>';   
  ?>
  </body>
</html>