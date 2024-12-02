using UnityEngine;
using UnityEngine.SceneManagement;
public class BulletShotSimulator : MonoBehaviour
{
    [SerializeField] private ChanonController _cc;
    void Start()
    {
        CreatePhysicsScene(); 
    }
    private void Update()
    {
        Simulation(m_ballPrefab, transform.position - Vector3.up, transform.forward * _cc.bulletVellocity);
    }

    private Scene m_simulationScene;
    private PhysicsScene m_physicsScene;
    [SerializeField] private Transform m_obstacleParent;

    private void CreatePhysicsScene()
    {
        m_simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D)); 
        m_physicsScene = m_simulationScene.GetPhysicsScene();

        foreach (Transform tf in m_obstacleParent)
        {
            var ghost = Instantiate(tf.gameObject, tf.position, tf.rotation); 
            ghost.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghost, m_simulationScene);
        }
    }

    [SerializeField] private GameObject m_ballPrefab;
    [SerializeField] private LineRenderer m_line;
    [SerializeField] private int m_iMaxPhysicsFrame;

    private void Simulation(GameObject _ballPrefab, Vector3 _pos, Vector3 _velocity)
    {
        var ghost = Instantiate(_ballPrefab, _pos, Quaternion.identity);
        ghost.GetComponent<Renderer>().enabled = false;
        SceneManager.MoveGameObjectToScene(ghost.gameObject, m_simulationScene);
        ghost.GetComponent<Rigidbody>().velocity = _velocity;

        m_line.positionCount = m_iMaxPhysicsFrame;

        for (int i = 0; i < m_iMaxPhysicsFrame; i++)
        {
            m_physicsScene.Simulate(Time.fixedDeltaTime);
            m_line.SetPosition(i, ghost.transform.position);
        }

        Destroy(ghost.gameObject);
    }
}

