using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.UnitsOfWork;
using Repository.Repositories;
using Common;

namespace FASTProcess
{
    public class AssetProcess : GenericProcess<FixAsset>
    {
        GenericUnitOfWork<FixAsset> _unitOfWork =
            new GenericUnitOfWork<FixAsset>();

        GenericUnitOfWork<vwFixAssetList> _assetList =
            new GenericUnitOfWork<vwFixAssetList>();

     
        public IQueryable<vwFixAssetList> GetAllFixAssets()
        {
            return _assetList.Repository.GetAllQueryable();
        }

        public vwFixAssetList GetFixAssetByAssetTag(string assetTag)
        {
            return _assetList.Repository.GetAllQueryable()
                .Where(m => m.AssetTag == assetTag).FirstOrDefault();


        }

        public vwFixAssetList GetFixAssetBySerialNumber(string serialNumber)
        {
            return _assetList.Repository.GetAllQueryable()
                .Where( m => m.SerialNumber == serialNumber).First();
        }

        public IQueryable<vwFixAssetList> GetFixAssetsByStatusID(int statusID)
        {
            return _assetList.Repository.GetAllQueryable()
                .Where(m => m.AssetStatusID == statusID);
        }

        public IQueryable<vwFixAssetList> GetFixAssetsByClassID(int classID)
        {
            return _assetList.Repository.GetAllQueryable()
                .Where(m => m.AssetClassID == classID);
        }

        public IQueryable<vwFixAssetList> GetFixAssetsByTypeID(int typeID)
        {
            return _assetList.Repository.GetAllQueryable()
                .Where(m => m.AssetTypeID == typeID);
        }

        public IQueryable<vwFixAssetList> GetFixAssetsByModel(string model)
        {
            return _assetList.Repository.GetAllQueryable()
                .Where(m => m.Model.Contains(model) == true);
        }

        public IQueryable<vwFixAssetList> GetFixAssetsByBrand(string brand)
        {
            return _assetList.Repository.GetAllQueryable()
                .Where(m => m.Brand.Contains(brand) == true);
        }


        //public IQueryable<vwFixAssetList> Search()
        //{

        //}

        public override int Add(FixAsset asset)
        {
            _unitOfWork.Repository.Insert(asset);
            if (_unitOfWork.Save() > 0)
            {
                _unitOfWork.LogSuccess(FASTConstant.AUDIT_ACTION_ADD + " Fix Asset", "", assetTag: asset.AssetTag, employeeID: UserID);
                return FASTConstant.RETURN_VAL_SUCCESS;
            }
            else
            {
                _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_ADD + " Fix Asset", "", assetTag: asset.AssetTag, employeeID: UserID);
                return FASTConstant.RETURN_VAL_FAILED;
            }
        }

        public override int Update(FixAsset asset)
        {
            _unitOfWork.Repository.Update(asset);
            if (_unitOfWork.Save() > 0)
            {
                _unitOfWork.LogSuccess(FASTConstant.AUDIT_ACTION_EDIT + " Fix Asset", "", assetTag: asset.AssetTag, employeeID: UserID);
                return FASTConstant.RETURN_VAL_SUCCESS;
            }
            else
            {
                _unitOfWork.LogFailure(FASTConstant.AUDIT_ACTION_EDIT + " Fix Asset", "", assetTag: asset.AssetTag, employeeID: UserID);
                return FASTConstant.RETURN_VAL_FAILED;
            }
        }

        public IQueryable<vwFixAssetList> SearchAsset(string anAssetTag = "", string aSerialNumber = "", int aStatusID = -1,string aModel = "",string aBrand = "")
        {

            IQueryable<vwFixAssetList> resultList;

            if ( aStatusID >= 0 )
            {
                resultList = _assetList.Repository.GetAllQueryable().Where(i => i.AssetStatusID == aStatusID);
            }
            else
            {
                resultList = _assetList.Repository.GetAllQueryable();    
            }

            if ( !string.IsNullOrEmpty(aModel))
            {
                resultList = resultList.Where(i => i.Model.Contains(aModel) == true);
            }

            if (!string.IsNullOrEmpty(aBrand))
            {
                resultList = resultList.Where(i => i.Brand.Contains(aBrand) == true);
            }

            if ( !String.IsNullOrEmpty(anAssetTag))
            {
                resultList = resultList.Where(i => i.AssetTag == anAssetTag);
            }

            if( !String.IsNullOrEmpty(aSerialNumber))
            {
                resultList = resultList.Where(i => i.SerialNumber == aSerialNumber);
            }


            return resultList;
        }







        
        
    }
}
