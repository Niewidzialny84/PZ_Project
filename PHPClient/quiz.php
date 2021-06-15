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

  if(isset($_POST['cat']) && !isset($_SESSION['iter'])) {
    $cat = $_POST['cat'];

    Protocol::beginQuiz($cat);
    $_SESSION['iter'] = 1;
    $_SESSION['score'] = 0;

    echo 'Question: '.$_SESSION['iter'].'/10</p>';

    $q = Protocol::getFirstQuestion();
    $_SESSION['corr'] = $q->correct;

    echo '<p>'.$q->question.'</p></br>';

    echo '<form method="post" action="quiz.php">';
    $arr = $q->answers;
    foreach($arr as $x) {
      echo '<input class="question" type="submit" name="answ" value="'.$x.'" /></br></br>';
    }
    echo '</form>';
  }

  if(isset($_SESSION['iter']) && isset($_POST['answ'])) {
    if($_SESSION['iter'] == 9) {
      $_SESSION['iter'] += 1;
      if($_POST['answ'] == $_SESSION['corr']) {
        $_SESSION['score'] += 10;
      } else {
        $_SESSION['score'] += 1;
      }
      Protocol::renderQuestion("summary.php");
    } else {
      $_SESSION['iter'] += 1;
      if($_POST['answ'] == $_SESSION['corr']) {
        $_SESSION['score'] += 10;
      } else {
        $_SESSION['score'] += 1;
      }
      Protocol::renderQuestion("quiz.php");
    }
  }
  
  echo '<form method="post" action="categories.php">
          <input type="submit" name="ret" value="Return" />
        </form>';   
  ?>
  </body>
</html>