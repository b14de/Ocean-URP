using System;
using OceanSystem;
using UnityEngine;

namespace DWP2
{
    public class FFTWaterDataProvider : WaterDataProvider
    {
        private OceanCollision    _oceanCollision;
        private OceanSimulation   _oceanSimulation;
        private int               _prevArraySize;
        private int               _hash = -1;
        private Vector3[]         _crestQueryHashArray; 
        private Vector3[]         _normals;
//      private IFlowProvider     _flowProvider;

        public override void Awake()
        {
            _oceanSimulation = GetComponent<OceanSimulation>();
            base.Awake();
        }

        public override bool SupportsWaterHeightQueries()
        {
            return true;
        }

        public override bool SupportsWaterNormalQueries()
        {
            return true;
        }

        public override bool SupportsWaterFlowQueries()
        {
            return false;
        }
        
        public override void GetWaterHeights(ref Vector3[] points, ref float[] waterHeights)
        {
            _oceanCollision ??= _oceanSimulation.Collision;
            
            if(_oceanCollision == null)
                return;
            
            int n = points.Length;
            _normals = new Vector3[n];

            var stepSize = 2.0f;
            for (var pointIndex = 0; pointIndex < points.Length; pointIndex++)
            {
                var point                = points[pointIndex];
                var waterDisplacedHeightPoint   = new Vector3(point.x, _oceanCollision.GetWaterHeight(point), point.z);
                waterHeights[pointIndex]        = waterDisplacedHeightPoint.y;

                // var point = points[pointIndex];
                // waterHeights[pointIndex] = _wavesGenerator.GetWaterHeight(point);
                //
                // var waterDisplaced = _wavesGenerator.GetWaterDisplacement(point);
                //
                // waterDisplaced = _wavesGenerator.GetWaterDisplacement(waterDisplaced);
                // waterDisplaced = _wavesGenerator.GetWaterDisplacement(waterDisplaced);
                // waterDisplaced = _wavesGenerator.GetWaterDisplacement(waterDisplaced);
                //
                // waterDisplaced.y = waterHeights[pointIndex];
                //
                // var pointR = new Vector3(waterDisplaced.x + stepSize, waterDisplaced.y, waterDisplaced.z);
                // var r = _wavesGenerator.GetWaterHeight(pointR);
                //
                // var pointL = new Vector3(waterDisplaced.x - stepSize, waterDisplaced.y, waterDisplaced.z);
                // var l = _wavesGenerator.GetWaterHeight(pointL);
                //
                // var pointT = new Vector3(waterDisplaced.x, waterDisplaced.y, points[pointIndex].z + stepSize);
                // var t = _wavesGenerator.GetWaterHeight(pointT);
                //
                // var pointB = new Vector3(waterDisplaced.x, waterDisplaced.y, points[pointIndex].z - stepSize);
                // var b = _wavesGenerator.GetWaterHeight(pointB);
               
                //Vector3 normal = new Vector3(2f*(r-l), 2f*(b-t), -4f).normalized;
                
                var dx = -Vector3.right * stepSize;
                var dz = Vector3.forward * stepSize;
                var px = new Vector3(waterDisplacedHeightPoint.x + dx.x, _oceanCollision.GetWaterHeight(waterDisplacedHeightPoint + dx), waterDisplacedHeightPoint.z + dx.z);
                var pz = new Vector3(waterDisplacedHeightPoint.x + dz.x, _oceanCollision.GetWaterHeight(waterDisplacedHeightPoint + dz), waterDisplacedHeightPoint.z + dz.z);

                Vector3 normal = Vector3.Cross(px - pz, waterDisplacedHeightPoint - pz).normalized;
                Debug.DrawLine(waterDisplacedHeightPoint, (waterDisplacedHeightPoint + normal * 1f), Color.red);
                _normals[pointIndex] = -normal;    //new Vector3(0,1,0);
            }
 
            // _collProvider.Query(
            //  _hash, 0, points,
            //  waterHeights, _normals, null);
            //
            // _prevArraySize = n;
        }

        public override void GetWaterNormals(ref Vector3[] points, ref Vector3[] waterNormals)
        {
            waterNormals = _normals; // Already queried in GetWaterHeights
        }

        public override float GetWaterHeightSingle(Vector3 point)
        {
            _oceanSimulation ??= GetComponent<OceanSimulation>();
            _oceanCollision  ??= _oceanSimulation.Collision;
            
            if(_oceanCollision == null)
                return 0;
            
            return _oceanCollision.GetWaterHeight(point);
        }
        
        // public override void GetWaterFlows(ref Vector3[] points, ref Vector3[] waterFlows)
        // {
        //  _flowProvider.Query(_hash, 0, points, waterFlows);
        // }
    }
}