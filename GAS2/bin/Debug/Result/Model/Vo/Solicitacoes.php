<?php 
namespace Ciente\Model\VO;

class Solicitacoes
{
const TABLE_NAME = "solicitacoes"; 
const KEY_ID = "id"; //integer
const ANO = "ano"; //integer
const NUMERO = "numero"; //bigint
const ID_SETOR = "idSetor"; //integer
const ID_SOLICITANTE = "idSolicitante"; //integer
const ID_SOLICITANTESEC = "idSolicitantesec"; //integer
const ID_EQUIPAMENTO = "idEquipamento"; //integer
const ID_USUARIO = "idUsuario"; //integer
const DESCRICAO = "descricao"; //text
const OBSERVACAO = "observacao"; //text
const DATA_ABERTURA = "dataAbertura"; //timestamp with time zone
const DATA_FECHAMENTO = "dataFechamento"; //timestamp with time zone
const PRIORIDADE = "prioridade"; //smallint
const STATUS = "status"; //smallint
const TABLE_FIELDS = [self::ANO, self::NUMERO, self::ID_SETOR, self::ID_SOLICITANTE, self::ID_SOLICITANTESEC, self::ID_EQUIPAMENTO, self::ID_USUARIO, self::DESCRICAO, self::OBSERVACAO, self::DATA_ABERTURA, self::DATA_FECHAMENTO, self::PRIORIDADE, self::STATUS]; 
public  $id; 
public  $ano; 
public  $numero; 
public  $idSetor; 
public  $idSolicitante; 
public  $idSolicitantesec; 
public  $idEquipamento; 
public  $idUsuario; 
public  $descricao; 
public  $observacao; 
public  $dataAbertura; 
public  $dataFechamento; 
public  $prioridade; 
public  $status; 

public  function getId ()
{
return $this->id;
}

public  function setId ( $id)
{
$this->id = $id;
}

public  function getAno ()
{
return $this->ano;
}

public  function setAno ( $ano)
{
$this->id = $ano;
}

public  function getNumero ()
{
return $this->numero;
}

public  function setNumero ( $numero)
{
$this->id = $numero;
}

public  function getIdSetor ()
{
return $this->idsetor;
}

public  function setIdSetor ( $idsetor)
{
$this->id = $idsetor;
}

public  function getIdSolicitante ()
{
return $this->idsolicitante;
}

public  function setIdSolicitante ( $idsolicitante)
{
$this->id = $idsolicitante;
}

public  function getIdSolicitantesec ()
{
return $this->idsolicitantesec;
}

public  function setIdSolicitantesec ( $idsolicitantesec)
{
$this->id = $idsolicitantesec;
}

public  function getIdEquipamento ()
{
return $this->idequipamento;
}

public  function setIdEquipamento ( $idequipamento)
{
$this->id = $idequipamento;
}

public  function getIdUsuario ()
{
return $this->idusuario;
}

public  function setIdUsuario ( $idusuario)
{
$this->id = $idusuario;
}

public  function getDescricao ()
{
return $this->descricao;
}

public  function setDescricao ( $descricao)
{
$this->id = $descricao;
}

public  function getObservacao ()
{
return $this->observacao;
}

public  function setObservacao ( $observacao)
{
$this->id = $observacao;
}

public  function getDataAbertura ()
{
return $this->dataabertura;
}

public  function setDataAbertura ( $dataabertura)
{
$this->id = $dataabertura;
}

public  function getDataFechamento ()
{
return $this->datafechamento;
}

public  function setDataFechamento ( $datafechamento)
{
$this->id = $datafechamento;
}

public  function getPrioridade ()
{
return $this->prioridade;
}

public  function setPrioridade ( $prioridade)
{
$this->id = $prioridade;
}

public  function getStatus ()
{
return $this->status;
}

public  function setStatus ( $status)
{
$this->id = $status;
}

}