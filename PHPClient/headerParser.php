<?php

abstract class Version{
  const V1 = 1;
}

abstract class Header{
  const ACK = 0;
  const ERR = 1;
  const DIS = 2;
  const LOG = 3;
  const SES = 4;
  const REG = 5;
  const LIS = 6;
  const ALI = 7;
  const QUI = 8;
  const QUE = 9;
  const NXT = 10;
  const END = 11;
  const STA = 12;
  const STR = 13;

  //Encode header and size to 3 byte array
  public static function encode($header, $size) {
    $byte1 = (((Version::V1 << 6) | $header) & 255);
    $byte2 = (($size >> 8) & 255);
    $byte3 = (($size) & 255);
    //echo $byte1.' '.$byte2.' '.$byte3;
    //return array(0=>$byte1,1=>$byte2,2=>$byte3);
    return (chr($byte1)).(chr($byte2)).(chr($byte3));
  }

  //Decode 3 byte array to header and size 
  public static function decode($data) {
    $version = ((ord($data[0]) >> 6) & 192);
    $header = ((ord($data[0])) & 63);
    $size = (((( ord($data[1]) << 8) & 65280) | (ord($data[2]) & 255)) & 65535);
    //echo '<br>'.$header.' '.$size;
    return array("header"=>$header,"size"=>$size);
  }
}

abstract class Protocol{
  public static function begin() {
    $port = 7777;
    $address = gethostbyname('tcp://molly.ovh');

    //$socket = socket_create(AF_INET,SOCK_STREAM,SOL_TCP);
    $socket = pfsockopen($address,$port,$errno, $errstr);
    if($socket === false) {
      echo "socket create error".$errstr;
    }
    //$_SESSION['so'] = var_dump($socket);
    
    $_SESSION['so'] = true;

    return $socket;
    //$result = socket_connect($socket,$address,$port);
    /*if($result === false) {
      echo "socket connect error";
    } else {
      //echo "OK";
      $_SESSION['so'] = var_dump($socket);
    }*/
  }

  public static function transfer($head, $data) {
    if(isset($_SESSION['so'])) {
      //$socket = ($_SESSION['so']);
      $socket = Protocol::begin();

      //socket_write($socket,$head,strlen($head));
      //socket_write($socket,$data,strlen($data));
      fwrite($socket,$head,strlen($head));
      fwrite($socket,$data,strlen($data));
      //stream_socket_sendto($socket,$head,strlen($head));
      //stream_socket_sendto($socket,$data,strlen($data));
    }
  }

  public static function recieve() {
    if(isset($_SESSION['so'])) {
      //$socket = ($_SESSION['so']);
      $socket = Protocol::begin();
      //$out = socket_read($socket,3);
      //$out = fread($socket,3);
      $out = stream_socket_recvfrom($socket,3);

      $head = Header::decode($out);
      //$out = socket_read($socket,$head['size']);
      //$out = fread($socket,$head['size']);
      $out = stream_socket_recvfrom($socket,$head['size']);

      $out = json_decode($out);
      return array("header"=>$head,"data"=>$out);
    }
  }

  public static function login($login, $password) {
    $a->login = $login;
    $a->password = $password;
    $data = json_encode($a);
    $head = Header::encode(Header::LOG,strlen($data));
    Protocol::transfer($head,$data);
    $out = Protocol::recieve();
    if ($out['header']['header'] == Header::SES) {
      return $out['data']->session;
    } else {
      return -1;
    }
  }

  public static function disconnect($msg) {
    $a->msg = $msg;
    $data = json_encode($a);
    $head = Header::encode(Header::DIS,strlen($data));
    Protocol::transfer($head,$data);

    if(isset($_SESSION['so'])) {
      $socket = Protocol::begin();
      //$socket = ($_SESSION['so']);
      //socket_close($socket);
      //fclose($socket);
      stream_socket_shutdown($socket,STREAM_SHUT_RDWR);
    }
  }

  public static function getCategories() {
    $a->msg = "send";
    $data = json_encode($a);
    $head = Header::encode(Header::ALI,strlen($data));
    Protocol::transfer($head,$data);
    $out = Protocol::recieve();
    if ($out['header']['header'] == Header::LIS) {
      return $out['data']->quizes;
    } else {
      return -1;
    }
  }

  public static function getStats() {
    $a->msg = "send";
    $data = json_encode($a);
    $head = Header::encode(Header::STR,strlen($data));
    Protocol::transfer($head,$data);
    $out = Protocol::recieve();
    if ($out['header']['header'] == Header::STA) {
      return $out['data']->stats;
    } else {
      return -1;
    }
  }

  public static function beginQuiz($category) {
    $a->category = $category;
    $data = json_encode($a);
    $head = Header::encode(Header::QUI,strlen($data));
    Protocol::transfer($head,$data);
  }

  public static function getQuestions() {
    $questions = array();
    $out = Protocol::recieve();

    $questions[0] = $out['data'];

    for($i=1;$i<10;$i++) {
      $a->msg = "send";
      $data = json_encode($a);
      $head = Header::encode(Header::NXT,strlen($data));
      Protocol::transfer($head,$data);
      $out = Protocol::recieve();
      $questions[$i] = $out['data'];
    }

    return $questions;
  }

  public static function getFirstQuestion() {
    $questions = array();
    $out = Protocol::recieve();

    return $out['data'];
  }

  public static function getNextQuestion() {
    $a->msg = "send";
    $data = json_encode($a);
    $head = Header::encode(Header::NXT,strlen($data));
    Protocol::transfer($head,$data);
    $out = Protocol::recieve();
    return $out['data'];
  }

  public static function endQuiz($score) {
    $a->score = $score;
    $data = json_encode($a);
    $head = Header::encode(Header::END,strlen($data));
    Protocol::transfer($head,$data);
    $out = Protocol::recieve();
    if ($out['header']['header'] == Header::ACK) {
      return true;
    } else {
      return false;
    }
  }

  public static function renderQuestion($target) {
    echo '<p>Question: '.$_SESSION['iter'].'/10</p>';

    $q = Protocol::getNextQuestion();
    $_SESSION['corr'] = $q->correct;

    echo '<p>'.$q->question.'</p></br>';

    echo '<form method="post" action="'.$target.'">';
    $arr = $q->answers;
    foreach($arr as $x) {
      echo '<input class="question" type="submit" name="answ" value="'.$x.'" /></br></br>';
    }
    echo '</form>';
  }
}

//Protocol::begin();
//Protocol::login("multiEryk","1234");
//print_r(Protocol::getCategories());
//Protocol::beginQuiz("Kolarstwo");
//print_r(Protocol::getQuestions());
//echo Protocol::endQuiz(50);
//print_r(Protocol::getStats());
//Protocol::disconnect("Bye");

?>