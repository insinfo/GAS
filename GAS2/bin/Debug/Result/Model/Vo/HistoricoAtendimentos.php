<?php 
namespace Ciente\Model\VO;

class HistoricoAtendimentos
{
const TABLE_NAME = "historico_atendimentos"; 
const KEY_ID = "id"; //integer
const ID_ATENDIMENTO = "idAtendimento"; //integer
const ID_USUARIO = "idUsuario"; //integer
const DATA = "data"; //timestamp with time zone
const COMENTARIO = "comentario"; //text
const TABLE_FIELDS = [self::ID_ATENDIMENTO, self::ID_USUARIO, self::DATA, self::COMENTARIO]; 
public  $id; 
public  $idAtendimento; 
public  $idUsuario; 
public  $data; 
public  $comentario; 

public  function getId ()
{
return $this->id;
}

public  function setId ( $id)
{
$this->id = $id;
}

public  function getIdAtendimento ()
{
return $this->idatendimento;
}

public  function setIdAtendimento ( $idatendimento)
{
$this->id = $idatendimento;
}

public  function getIdUsuario ()
{
return $this->idusuario;
}

public  function setIdUsuario ( $idusuario)
{
$this->id = $idusuario;
}

public  function getData ()
{
return $this->data;
}

public  function setData ( $data)
{
$this->id = $data;
}

public  function getComentario ()
{
return $this->comentario;
}

public  function setComentario ( $comentario)
{
$this->id = $comentario;
}

}