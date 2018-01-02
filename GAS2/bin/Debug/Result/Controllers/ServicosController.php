<?php 
namespace Ciente\Controller;

use \Slim\Http\Request as Req;
use \Slim\Http\Response as Resp;
use \Exception;
use Ciente\Util\DBLayer;
use Ciente\Util\Utils;
use Ciente\Model\VO\Servicos;

class ServicosController
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
            $query = DBLayer::Connect()->table(Servicos::TABLE_NAME)
     ->where(function($query) use($request, $search){
     $query->orWhere(Servicos::KEY_ID, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Servicos::ID_TIPO_SERVICO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Servicos::ID_CONTRATO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Servicos::CODIGO_REFERENCIA, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Servicos::NOME, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Servicos::DESCRICAO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Servicos::PRAZO_BAIXA, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Servicos::PRAZO_NORMAL, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Servicos::PRAZO_ALTA, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Servicos::PRAZO_URGENTE, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Servicos::ATIVO, DBLayer::OPERATOR_ILIKE, $search);
  });
$totalRecords = $query->count();
            $data = $query->orderBy(Servicos::KEY_ID, 'asc')
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
                $idServicos = $request->getAttribute('idServicos');
                $formData = Utils::filterArrayByArray($request->getParsedBody(), Servicos::TABLE_FIELDS);

                if ($idServicos)
                {
                    DBLayer::Connect()->table(Servicos::TABLE_NAME)->update($formData);
                }
                else
                {
                    DBLayer::Connect()->table(Servicos::TABLE_NAME)->insertGetId($formData);
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
                $idServicos = $request->getAttribute('Servicos');
                $servicos = DBLayer::Connect()->table(Servicos::TABLE_NAME)
                    ->where(Servicos::KEY_ID,DBLayer::OPERATOR_EQUAL,$idServicos)->first();
                return $response->withStatus(200)->withJson($servicos);
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
                    $servicos = DBLayer::Connect()->table(Servicos::TABLE_NAME)
                        ->where(Servicos::KEY_ID,DBLayer::OPERATOR_EQUAL,$id)->first();

                    if ($servicos)
                    {
                        if (DBLayer::Connect()->table(Servicos::TABLE_NAME)->delete($id))
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