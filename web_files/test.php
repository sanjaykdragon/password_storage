<?php
    $server_username = "root";
    $server_password = "";
    $server_dbname = "pw_mgr";
    $server_server = "localhost";

    try {
        $conn = new PDO('mysql:host=' . $server_server . ';dbname=' . $server_dbname, $server_username, $server_password, [PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC]);
    } catch (Exception $e) {
        die('Error connecting to database: ' . $e);
    }
    
    if(!isset($_POST["option"]))
        die(json_encode(array("error" => "option not set")));
        
    if($_POST["option"] == "save")
    {
        $insert_credentials = $conn->prepare("INSERT INTO info (site, username, password, time) VALUES (:site, :username, :password, :time);");
        $insert_credentials->bindValue(":site", $_POST["site"]);
        $insert_credentials->bindValue(":username", $_POST["username"]);
        $insert_credentials->bindValue(":password", $_POST["password"]);
        $insert_credentials->bindValue(":time", time());
        if($insert_credentials->execute())
		{
			die(json_encode(array("status" => "success")));
		}
		else
		{
			die(json_encode(array("status" => "failed", "detail" => "failed to add to db")));
		}
    }
	
    if($_POST["option"] == "get_list")
	{
		$get_credentials = $conn->prepare("SELECT * FROM info");
		$creds = array("status" => "success", "values" => array());
		if($get_credentials->execute())
		{
			while ($row = $get_credentials->fetch(PDO::FETCH_ASSOC)) {
				$creds["values"][] = $row;
			}
			die(json_encode($creds));
		}
		else
		{
			die(json_encode(array("status" => "failed", "detail" => "failed when getting from db")));
		}
	}
?>