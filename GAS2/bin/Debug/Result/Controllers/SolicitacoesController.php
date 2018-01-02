<?php 
namespace Ciente\Controller;

use \Slim\Http\Request as Req;
use \Slim\Http\Response as Resp;
use \Exception;
use Ciente\Util\DBLayer;
use Ciente\Util\Utils;
use Ciente\Model\VO\Solicitacoes;

class SolicitacoesController
{

public static function getAll (Req $request,Resp $response)
{

            try
            {
            $parametros = $request->getParsedBody();               
            $draw = $parametros['draw'];
            $limit = $parametros['length'];
            $offset = $parametros['start'];
            $search = '%'.$parametros['search'].'%';
            $query = DBLayer::Connect()->table(Solicitacoes::TABLE_NAME)
     ->where(function($query) use($request, $search){
     $query->orWhere(Solicitacoes::KEY_ID, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Solicitacoes::ANO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Solicitacoes::NUMERO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Solicitacoes::ID_SETOR, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Solicitacoes::ID_SOLICITANTE, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Solicitacoes::ID_SOLICITANTESEC, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Solicitacoes::ID_EQUIPAMENTO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Solicitacoes::ID_USUARIO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Solicitacoes::DESCRICAO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Solicitacoes::OBSERVACAO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Solicitacoes::DATA_ABERTURA, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Solicitacoes::DATA_FECHAMENTO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Solicitacoes::PRIORIDADE, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Solicitacoes::STATUS, DBLayer::OPERATOR_ILIKE, $search);
  });
$totalRecords = $query->count();
            $data = $query->orderBy(Solicitacoes::KEY_ID, 'asc')
            ->take($limit)->offset($offset)->get();
            $result['draw'] = $draw;
            $result['recordsFiltered'] = $totalRecords;
            $result['recordsTotal'] = $totalRecords;
            $result['data'] = $data;
            }
            catch (Exception $e)
            {
                    return $response->withStatus(400)->withJson((['message' => 'Ouve um erro desconhecido.', 'exception' => $e->getMessage(), 'line' => $e->getLine(), 'file' => $e->getFile()]));
            }
            return $response->withStatus(200)->withJson($result); 
}

public static function save (Req $request,Resp $response)
{

            try
            {
                $idSolicitacoes = $request->getAttribute('idSolicitacoes');
                $formData = Utils::filterArrayByArray($request->getParsedBody(), Solicitacoes::TABLE_FIELDS);

                if ($idSolicitacoes)
                {
                    DBLayer::Connect()->table(Solicitacoes::TABLE_NAME)->update($formData);
                }
                else
                {
                    DBLayer::Connect()->table(Solicitacoes::TABLE_NAME)->insertGetId($formData);
                }
            }
            catch (Exception $e)
            {
                return $response->withStatus(400)->withJson((['message' => 'Ouve um erro desconhecido.', 'exception' => $e->getMessage(), 'line' => $e->getLine(), 'file' => $e->getFile()]));
            }
            return $response->withStatus(200)->withJson((['message' => 'Salvo com sucesso']));
}

public static function get (Req $request,Resp $response)
{
 
            try
            {
                $idSolicitacoes = $request->getAttribute('Solicitacoes');
                $solicitacoes = DBLayer::Connect()->table(Solicitacoes::TABLE_NAME)
                    ->where(Solicitacoes::KEY_ID,DBLayer::OPERATOR_EQUAL,$idSolicitacoes)->first();
                return $response->withStatus(200)->withJson($solicitacoes);
            }
            catch (Exception $e)
            {
                return $response->withStatus(400)->withJson(['message' => 'Ouve um erro desconhecido.', 'exception' => $e->getMessage(), 'line' => $e->getLine(), 'file' => $e->getFile()]);
            }
}

public static function delete (Req $request,Resp $response)
{

            try
            {
                $formData = $request->getParsedBody();
                $ids = $formData['ids'];
                $idsCount = count($ids);
                $itensDeletadosCount = 0;
                foreach ($ids as $id)
                {
                    $solicitacoes = DBLayer::Connect()->table(Solicitacoes::TABLE_NAME)
                        ->where(Solicitacoes::KEY_ID,DBLayer::OPERATOR_EQUAL,$id)->first();

                    if ($solicitacoes)
                    {
                        if (DBLayer::Connect()->table(Solicitacoes::TABLE_NAME)->delete($id))
                        {
                            $itensDeletadosCount++;
                        }
                    }
                }
                if ($itensDeletadosCount == $idsCount)
                {
                    return response()->json(['message' => 'Todos os itens foram deletados com sucesso']);
                }
                else if ($itensDeletadosCount > 0)
                {
                    return response()->json(['message' => 'Nem todos os itens foram deletados com sucesso']);
                }
                else
                {
                    return $response->withStatus(200)->withJson((['message' => 'Nenhum dos itens foram deletados']));
                }
            }
            catch (Exception $e)
            {
                return $response->withStatus(400)->withJson(['message' => 'Ouve um erro desconhecido.', 'exception' => $e->getMessage(), 'line' => $e->getLine(), 'file' => $e->getFile()]);
            }
}

}