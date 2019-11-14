using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager> {

	protected LevelManager() {}

	private WaveManager waveManager;

	// Camera
	public CameraFollowObject camFollowHero;

	// Model
	public GameObject heroModel;

	// Input Handle
	private PlayerInput playerInput;

	// Level information
	private bool isGameOver;

	private Hero player;
	public Hero Player {
		get {
			return player;
		}
		set {
			player = value;
			if (player != null) {
				UIManager.Instance.SetPlayerStatusBar (Attribute.Energy.HEALTH,  player.healthAtt );
				UIManager.Instance.SetPlayerStatusBar (Attribute.Energy.MANA,    player.manaAtt   );
				UIManager.Instance.SetPlayerStatusBar (Attribute.Energy.STAMINA, player.staminaAtt);

				for (int i = 0; i < player.Abilities.Count; i++) {
					UIManager.Instance.SetPlayerAbilityIcon(i, player.Abilities[i]);
				}
			}
		}
	}

	void Start()
	{
		playerInput = GetComponent<PlayerInput>();
		waveManager = GetComponent<WaveManager>();

		NewGame();
	}

	void Update() 
	{
		if (!isGameOver) {
			//if (!Player.gameObject.activeInHierarchy) {
			//	isGameOver = true;
			//	//waveManager.ForceKill();
			//}
			// or LastWaveClear calls. It will be call automatic
			// when no monster left on the level
		} else {
			UIManager.Instance.restartButton.SetActive(true);
		}
	}

	public void NewGame()
	{
		// Create hero
		if (player == null || !player.gameObject.activeInHierarchy)
			Player = SpawnHero();
		//playerInput.player = Player;
		camFollowHero.target = Player.transform;

		//// =====

		//waveManager.ForceKill();
		//StartCoroutine( waveManager.Call() );

		//// =====

		//// Re-open the game
		//UIManager.Instance.restartButton.SetActive(false);
		//isGameOver = false;
	}

	public void LastWaveClear()
	{
		isGameOver = true;
	}

	public Hero SpawnHero()
	{
		Hero clone = ObjectPool.Instance.GetPooledObject(heroModel).GetComponent<Hero>();
		clone.name = "Hero";
		return clone;
	}

}
