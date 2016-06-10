using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_Profession : ScriptableObject
{	
	public List<Param> param = new List<Param> ();

	[System.SerializableAttribute]
	public class Param
	{
		
		public int ProfessionID;
		public int ActiveFlg;
		public string Name;
		public int Level;
		public int HP;
		public int Speed;
		public int PhysicsATK;
		public int PhysiceDEF;
		public int MagicATK;
		public int MagicDEF;
		public double HPExt;
		public double SpeedExt;
		public double PhysicsATKExt;
		public double PhysiceDEFExt;
		public double MagicATKExt;
		public double MagicDEFExt;
		public double Luck;
		public double CriticalRate;
		public double CriticalPower;
		public double FireATK;
		public double NatureATK;
		public double IceATK;
		public double EarthATK;
		public double ThunderATK;
		public double WaterATK;
		public double LifeATK;
		public double FireDEF;
		public double NatureDEF;
		public double IceDEF;
		public double EarthDEF;
		public double ThunderDEF;
		public double WaterDEF;
		public double LifeDEF;
	}
}