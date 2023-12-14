 using UnityEngine ;

public class CubeCollision : MonoBehaviour {
   Cube cube ;
    public AudioSource Soundeffect;
   private void Awake () {
      cube = GetComponent<Cube> () ;
   }
    private void Start()
    {
        Soundeffect = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter (Collision collision) {
      Cube otherCube = collision.gameObject.GetComponent <Cube> () ;

      if (otherCube != null && cube.CubeID > otherCube.CubeID) {
 
         if (cube.CubeNumber == otherCube.CubeNumber) {
            Vector3 contactPoint = collision.contacts [ 0 ].point ;

      
            if (otherCube.CubeNumber < CubeSpawner.Instance.maxCubeNumber) {
      
               Cube newCube = CubeSpawner.Instance.Spawn (cube.CubeNumber * 2, contactPoint + Vector3.up * 1.6f) ;

               float pushForce = 2.5f ;
               newCube.CubeRigidbody.AddForce (new Vector3 (0, .3f, 1f) * pushForce, ForceMode.Impulse) ;

              
               float randomValue = Random.Range (-20f, 20f) ;
               Vector3 randomDirection = Vector3.one * randomValue ;
               newCube.CubeRigidbody.AddTorque (randomDirection) ;
            }

            Collider[] surroundedCubes = Physics.OverlapSphere (contactPoint, 2f) ;
            float explosionForce = 400f ;
            float explosionRadius = 1.5f ;

            foreach (Collider coll in surroundedCubes) {
               if (coll.attachedRigidbody != null)
                  coll.attachedRigidbody.AddExplosionForce (explosionForce, contactPoint, explosionRadius) ;
            }

            FX.Instance.PlayCubeExplosionFX (contactPoint, cube.CubeColor) ;
                Soundeffect.Play();
       
                CubeSpawner.Instance.DestroyCube (cube);
                CubeSpawner.Instance.DestroyCube (otherCube);
            }
      }
   }
}
