<?php 
namespace Ciente\Model\VO;

class Atendimentos
{
const TABLE_NAME = "atendimentos"; 
const KEY_ID = "id"; //integer
const ID_SOLICITACAO = "idSolicitacao"; //integer
const ID_SETOR = "idSetor"; //integer
const ID_SERVICO = "idServico"; //integer
const ID_USUARIO = "idUsuario"; //integer
const DATA_ENCAMINHAMENTO = "dataEncaminhamento"; //timestamp with time zone
const DATA_INICIO_ATENDIMENTO = "dataInicioAtendimento"; //timestamp with time zone
const DATA_FIM_ATENDIMENTO = "dataFimAtendimento"; //timestamp with time zone
const STATUS = "status"; //smallint
const TABLE_FIELDS = [self::ID_SOLICITACAO, self::ID_SETOR, self::ID_SERVICO, self::ID_USUARIO, self::DATA_ENCAMINHAMENTO, self::DATA_INICIO_ATENDIMENTO, self::DATA_FIM_ATENDIMENTO, self::STATUS]; 
public  $id; 
public  $idSolicitacao; 
public  $idSetor; 
public  $idServico; 
public  $idUsuario; 
public  $dataEncaminhamento; 
public  $dataInicioAtendimento; 
public  $dataFimAtendimento; 
public  $status; 

public  function getId ()
{
return $this->id;
}

public  function setId ( $id)
{
$this->id = $id;
}

public  function getIdSolicitacao ()
{
return $this->idsolicitacao;
}

public  function setIdSolicitacao ( $idsolicitacao)
{
$this->id = $idsolicitacao;
}

public  function getIdSetor ()
{
return $this->idsetor;
}

public  function setIdSetor ( $idsetor)
{
$this->id = $idsetor;
}

public  function getIdServico ()
{
return $this->idservico;
}

public  function setIdServico ( $idservico)
{
$this->id = $idservico;
}

public  function getIdUsuario ()
{
return $this->idusuario;
}

public  function setIdUsuario ( $idusuario)
{
$this->id = $idusuario;
}

public  function getDataEncaminhamento ()
{
return $this->dataencaminhamento;
}

public  function setDataEncaminhamento ( $dataencaminhamento)
{
$this->id = $dataencaminhamento;
}

public  function getDataInicioAtendimento ()
{
return $this->datainicioatendimento;
}

public  function setDataInicioAtendimento ( $datainicioatendimento)
{
$this->id = $datainicioatendimento;
}

public  function getDataFimAtendimento ()
{
return $this->datafimatendimento;
}

public  function setDataFimAtendimento ( $datafimatendimento)
{
$this->id = $datafimatendimento;
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