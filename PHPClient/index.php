<?php
  session_start();

  require_once('headerParser.php');

  if((isset($_SESSION['logged'])) && ($_SESSION['logged']==true))
  {
    header('Location: main.php');
		exit();
  }
?>

<html>
  <head>
    <title>Test</title>
    <link rel="stylesheet" href="styles.css">
  </head>
  <body>
    <form action="login.php" method="post">
      Login:  <input type="text" name="login" />
      Password: <input type="password" name="passwd" />
      <input type="submit" value="Log In" />
    </form>
<?php
	if(isset($_SESSION['errr'])) {
    echo $_SESSION['errr'];
  }
?>
  </body>
</html>