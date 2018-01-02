<?php 
namespace Ciente\Model\VO;

class TiposServico
{
const TABLE_NAME = "tipos_servico"; 
const KEY_ID = "id"; //integer
const ID_SETOR = "idSetor"; //integer
const NOME = "nome"; //character varying
const DESCRICAO = "descricao"; //text
const PRAZO_ATENDIMENTO_INICIAL = "prazoAtendimentoInicial"; //integer
const ATIVO = "ativo"; //boolean
const TABLE_FIELDS = [self::ID_SETOR, self::NOME, self::DESCRICAO, self::PRAZO_ATENDIMENTO_INICIAL, self::ATIVO]; 
public  $id; 
public  $idSetor; 
public  $nome; 
public  $descricao; 
public  $prazoAtendimentoInicial; 
public  $ativo; 

public  function getId ()
{
return $this->id;
}

public  function setId ( $id)
{
$this->id = $id;
}

public  function getIdSetor ()
{
return $this->idsetor;
}

public  function setIdSetor ( $idsetor)
{
$this->id = $idsetor;
}

public  function getNome ()
{
return $this->nome;
}

public  function setNome ( $nome)
{
$this->id = $nome;
}

public  function getDescricao ()
{
return $this->descricao;
}

public  function setDescricao ( $descricao)
{
$this->id = $descricao;
}

public  function getPrazoAtendimentoInicial ()
{
return $this->prazoatendimentoinicial;
}

public  function setPrazoAtendimentoInicial ( $prazoatendimentoinicial)
{
$this->id = $prazoatendimentoinicial;
}

public  function getAtivo ()
{
return $this->ativo;
}

public  function setAtivo ( $ativo)
{
$this->id = $ativo;
}

}