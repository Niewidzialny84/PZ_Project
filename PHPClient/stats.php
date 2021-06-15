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

  $stats = Protocol::getStats();

  echo '
        <table>
          <tr>
            <th>Category</th>
            <th>Score</th>
          </tr>
  ';
  foreach($stats as $x => $val) {
    echo '<tr><td>'.$val->category.'</td><td>'.$val->score.'</td></tr>';
  }
  echo '</table>';

  echo '<form method="post" action="main.php">
          <input type="submit" value="Return" />
        </form>';   
  ?>
  </body>
</html>