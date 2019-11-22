using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamTag : MonoBehaviour
{
	[SerializeField]
	private int    teamIndex;
	private Entity owner;

	public int Team {
		get {
			return teamIndex;
		}
	}

	public Entity Owner {
		get {
			return owner;
		}

		set {
			owner = value;
			this.SetTeam(owner.GetComponent<TeamTag>().Team);
		}
	}

	/// <summary>
	/// Sets entity's team to i
	/// </summary>
	/// <param name="i">The index.</param>
	public void SetTeam(int i)
	{
		teamIndex = i;
	}

	/// <summary>
	/// Sets entity's team to target object's team
	/// </summary>
	/// <param name="targetObject">Target object.</param>
	public void SetTeam(TeamTag targetObject)
	{
		teamIndex = targetObject.teamIndex;
	}

	/// <summary>
	/// Sets entity's team to target object's team
	/// </summary>
	/// <param name="targetObject">Target object.</param>
	public void SetTeam(Entity targetObject)
	{
		TeamTag t = targetObject.GetComponent<TeamTag>();

		if (t == null)
			Debug.LogError("[" + targetObject.name + "] doesn't have a team but ["
				+ gameObject.name + "] trying to set team tag from that");

		teamIndex = t.teamIndex;
	}

	/// <summary>
	/// Sets entity's team to target object's team
	/// </summary>
	/// <param name="targetObject">Target object.</param>
	public void SetTeam(GameObject targetObject)
	{
		TeamTag t = targetObject.GetComponent<TeamTag>();

		if (t == null)
			Debug.LogError("[" + targetObject.name + "] doesn't have a team but ["
				+ gameObject.name + "] trying to set team tag from that");

		teamIndex = t.teamIndex;
	}

	/// <summary>
	/// Compare the team of team A and team B.
	/// </summary>
	/// <returns>true if their team are equal</returns>
	/// <param name="teamA">Team A.</param>
	/// <param name="teamB">Team B.</param>
	public static bool CompareTeam(TeamTag teamA, TeamTag teamB)
	{
		return teamA.Team == teamB.Team ? true : false;
	}

	/// <summary>
	/// Compare the team of team A and team B.
	/// </summary>
	/// <returns>
	/// 	True if their team are equal.
	/// 	False if their team aren't equal 
	///		or one of them don't have Team Tag. 
	/// </returns>
	/// <param name="objA">Game Object A.</param>
	/// <param name="objB">Game Object B.</param>
	public static bool CompareTeam(GameObject objA, GameObject objB)
	{
		TeamTag teamA = objA.GetComponent<TeamTag>();
		if (teamA == null) {
			Debug.LogError("[" + objA.name + "] doesn't have a team but being tried comparing");
			return false;
		}

		TeamTag teamB = objB.GetComponent<TeamTag>();
		if (teamB == null) {
			Debug.LogError("[" + objA.name + "] doesn't have a team but being tried comparing");
			return false;
		}

		return teamA.Team == teamB.Team ? true : false;
	}
}
