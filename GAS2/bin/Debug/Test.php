<?php 
namespace Ciente\Model\VO;
class Test
{
const TABLE_NAME = "test"; 
const KEY_ID = "id"; \\integer
const NOME = "nome"; \\character varying
const IDADE = "idade"; \\integer
const TELEFONE = "telefone"; \\character varying
const TABLE_FIELDS = [self::NOME, self::IDADE, self::TELEFONE]; 
public  $id; 
public  $nome; 
public  $idade; 
public  $telefone; 

public  function getId ()
{
return $this->id;
}

public  function setId ( $id)
{
$this->id = $id;
}

public  function getNome ()
{
return $this->nome;
}

public  function setNome ( $nome)
{
$this->id = $nome;
}

public  function getIdade ()
{
return $this->idade;
}

public  function setIdade ( $idade)
{
$this->id = $idade;
}

public  function getTelefone ()
{
return $this->telefone;
}

public  function setTelefone ( $telefone)
{
$this->id = $telefone;
}

}