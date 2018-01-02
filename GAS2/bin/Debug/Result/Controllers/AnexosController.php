<?php 
namespace Ciente\Controller;

use \Slim\Http\Request as Req;
use \Slim\Http\Response as Resp;
use \Exception;
use Ciente\Util\DBLayer;
use Ciente\Util\Utils;
use Ciente\Model\VO\Anexos;

class AnexosController
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
            $query = DBLayer::Connect()->table(Anexos::TABLE_NAME)
     ->where(function($query) use($request, $search){
     $query->orWhere(Anexos::KEY_ID, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Anexos::ID_SOLICITACAO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Anexos::ID_ATENDIMENTO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Anexos::ID_COMUNICACAO_INTERNA, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Anexos::CAMINHO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Anexos::DATA, DBLayer::OPERATOR_ILIKE, $search);
  });
$totalRecords = $query->count();
            $data = $query->orderBy(Anexos::KEY_ID, 'asc')
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
                $idAnexos = $request->getAttribute('idAnexos');
                $formData = Utils::filterArrayByArray($request->getParsedBody(), Anexos::TABLE_FIELDS);

                if ($idAnexos)
                {
                    DBLayer::Connect()->table(Anexos::TABLE_NAME)->update($formData);
                }
                else
                {
                    DBLayer::Connect()->table(Anexos::TABLE_NAME)->insertGetId($formData);
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
                $idAnexos = $request->getAttribute('Anexos');
                $anexos = DBLayer::Connect()->table(Anexos::TABLE_NAME)
                    ->where(Anexos::KEY_ID,DBLayer::OPERATOR_EQUAL,$idAnexos)->first();
                return $response->withStatus(200)->withJson($anexos);
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
                    $anexos = DBLayer::Connect()->table(Anexos::TABLE_NAME)
                        ->where(Anexos::KEY_ID,DBLayer::OPERATOR_EQUAL,$id)->first();

                    if ($anexos)
                    {
                        if (DBLayer::Connect()->table(Anexos::TABLE_NAME)->delete($id))
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