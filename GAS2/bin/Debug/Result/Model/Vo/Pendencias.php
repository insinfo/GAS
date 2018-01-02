<?php 
namespace Ciente\Model\VO;

class Pendencias
{
const TABLE_NAME = "pendencias"; 
const KEY_ID = "id"; //integer
const ID_ATENDIMENTO = "idAtendimento"; //integer
const ID_HISTORICO_ATENDIMENTO = "idHistoricoAtendimento"; //integer
const DATA_INICIO = "dataInicio"; //timestamp with time zone
const DATA_FIM = "dataFim"; //timestamp with time zone
const TABLE_FIELDS = [self::ID_ATENDIMENTO, self::ID_HISTORICO_ATENDIMENTO, self::DATA_INICIO, self::DATA_FIM]; 
public  $id; 
public  $idAtendimento; 
public  $idHistoricoAtendimento; 
public  $dataInicio; 
public  $dataFim; 

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

public  function getIdHistoricoAtendimento ()
{
return $this->idhistoricoatendimento;
}

public  function setIdHistoricoAtendimento ( $idhistoricoatendimento)
{
$this->id = $idhistoricoatendimento;
}

public  function getDataInicio ()
{
return $this->datainicio;
}

public  function setDataInicio ( $datainicio)
{
$this->id = $datainicio;
}

public  function getDataFim ()
{
return $this->datafim;
}

public  function setDataFim ( $datafim)
{
$this->id = $datafim;
}

}