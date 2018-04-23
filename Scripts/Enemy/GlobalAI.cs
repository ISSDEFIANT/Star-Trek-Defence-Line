using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GlobalAI : MonoBehaviour {
	public bool NeuralNetwork;

	public bool Federation;
	public bool Borg;

	public bool AI;
	public bool FreandAI;

	public bool Agress;
	public bool Passive;

	private GlobalDB _GDB;
	private Select _SEL;

	public int PlayerNumber;
	public Color PlayerColor;

	[HideInInspector]
	public bool NeedStarBaseLock;
	[HideInInspector]
	public bool NeedMinesLock;
	[HideInInspector]
	public bool NeedDock1Lock;
	[HideInInspector]
	public bool NeedDock2Lock;
	[HideInInspector]
	public bool NeedSciStationLock;
	[HideInInspector]
	public bool NeedSensorsLock;
	[HideInInspector]
	public bool NeedPhaserTurretsLock;
	[HideInInspector]
	public bool NeedTorpedoTurretsLock;
	[HideInInspector]
	public bool NeedTradingStationLock;
	[HideInInspector]
	public bool NeedDefenceStationLock;

	public bool NeedStarBase;
	public bool NeedMines;
	public bool NeedDock1;
	public bool NeedDock2;
	public bool NeedSciStation;
	public bool NeedSensors;
	public bool NeedPhaserTurrets;
	public bool NeedTorpedoTurrets;
	public bool NeedTradingStation;
	public bool NeedDefenceStation;

	public bool NeedBuilders;
	public bool NeedTransports;
	public bool NeedTitaniumMiners;
	public bool NeedDilithiumMiners;

	public bool NeedDefenceFleet;
	public bool NeedScoutFleet;
	public bool NeedAttackFleet;
	public bool NeedAttackFleet2;
	public bool NeedAttackFleet3;

	public List<GameObject> FreeBuilders;
	public List<GameObject> Builders;
	public List<GameObject> TitaniumMiners;
	public List<GameObject> DilithiumMiners;

	public List<GameObject> DefenceFleet;
	public List<GameObject> ScoutFleet;
	public List<GameObject> AttackFleet;
	public List<GameObject> AttackFleet2;
	public List<GameObject> AttackFleet3;

	public List<GameObject> StarBases;
	public List<GameObject> Mines;
	public List<GameObject> Docks1;
	public List<GameObject> Docks2;
	public List<GameObject> SciStations;
	public List<GameObject> Sensors;
	public List<GameObject> PhaserTurrets;
	public List<GameObject> TorpedoTurrets;
	public List<GameObject> TradingStations;
	public List<GameObject> DefenceStations;

	public List<GameObject> EnemyObjectsIsVisible;

	private float UpdateTimer = 1;

	public int BaseLevel;
	private float BaseUpTimer = 600;

	public float UpdateDBTimer;

	public float Titanium;
	public float Dilithium;
	public float Crew;

	private bool ChangeBool;
	private float FleetControlTimer;

	private int AttackFleetShipRange;
	private int DefenceFleetShipRange;
	private int ScoutFleetShipRange;


	private bool BaseScoutingCompleat;

	void Awake(){
		UpdateDBTimer = 1;
		EnemyObjectsIsVisible = new List<GameObject> ();
	}

		void Start () {
		hasFailed = false;

		neuralnet = new NNet ();
		neuralnet.CreateNet (2, 8, 8, 8);

			_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
			_SEL = GameObject.FindGameObjectWithTag("MainUI").GetComponent<Select>();
		}

		// Update is called once per frame
		void Update () {
		if (PlayerNumber == 2) {
			PlayerColor = _GDB.gameObject.GetComponent<PlayerColour> ().PlayerColor2;
		}
		if (PlayerNumber == 3) {
			PlayerColor = _GDB.gameObject.GetComponent<PlayerColour> ().PlayerColor3;
		}
		if (PlayerNumber == 4) {
			PlayerColor = _GDB.gameObject.GetComponent<PlayerColour> ().PlayerColor4;
		}
		if (PlayerNumber == 5) {
			PlayerColor = _GDB.gameObject.GetComponent<PlayerColour> ().PlayerColor5;
		}
		if (PlayerNumber == 6) {
			PlayerColor = _GDB.gameObject.GetComponent<PlayerColour> ().PlayerColor6;
		}
		if (PlayerNumber == 7) {
			PlayerColor = _GDB.gameObject.GetComponent<PlayerColour> ().PlayerColor7;
		}
		if (PlayerNumber == 8) {
			PlayerColor = _GDB.gameObject.GetComponent<PlayerColour> ().PlayerColor8;
		}
	}

	void LateUpdate(){
		
	}
	public void FleetControlEvent(){

	}

	public void UpdateDB(){
		
	}

	public void ScoutFindEnemyShip(GameObject Enemy, GameObject ShipThatFound){

	}
	public void ScoutFindEnemyBase(GameObject Enemy, GameObject ShipThatFound){

	}

	public void NeedHelp(GameObject Enemy){
		foreach (GameObject Ships in AttackFleet2) {
			Ships.GetComponent<ShipAI> ().Attack (Enemy);
		}
		foreach (GameObject Ships in AttackFleet3) {
			Ships.GetComponent<ShipAI> ().Attack (Enemy);
		}
	}

	public void MustGoAway(){
		if (Federation) {
			foreach (GameObject s1 in AttackFleet) {
				s1.GetComponent<MoveComponent> ().Movement (StarBases [0].transform.position + new Vector3 (100, 0, 100));
				s1.GetComponent<ShipAI> ().DontReaction = true;
			}
			foreach (GameObject s2 in AttackFleet2) {
				s2.GetComponent<MoveComponent> ().Movement (StarBases [0].transform.position + new Vector3 (100, 0, 100));
				s2.GetComponent<ShipAI> ().DontReaction = true;
			}
			foreach (GameObject s3 in AttackFleet3) {
				s3.GetComponent<MoveComponent> ().Movement (StarBases [0].transform.position + new Vector3 (100, 0, 100));
				s3.GetComponent<ShipAI> ().DontReaction = true;
			}
		}
	}

	public void EnemyBaseScoutingCompleat(GameObject Enemy, GameObject ShipThatFound){
		foreach (GameObject Ships in AttackFleet) {
			Ships.GetComponent<ShipAI> ().Attack (Enemy);
		}
		ShipThatFound.GetComponent<MoveComponent> ().Movement (StarBases [0].transform.position + new Vector3 (100, 0, 100));
		ShipThatFound.GetComponent<ShipAI> ().DontReaction = true;
	}

	bool FindInFreeBuildersList (GameObject obj)
	{
		foreach (GameObject selObj in FreeBuilders) {
			if (selObj == obj)
				return true;
		}
		return false;
	}

	public void AIBuilderTargeting(GameObject Building){
		FreeBuilders [0].GetComponent<Builder> ().BuilderTarget = Building;
	}




	//NeuaralNet
	int OldStarBases;
	int CurStarBases;
	public NNet neuralnet;
	public bool hasFailed;



	public float Normalise(float i){
		float depth = i / 100;
		return 1 - depth;
	}
	public void Attach(NNet net){
		neuralnet = net;
	}

	public float Clamp (float val, float min, float max){
		if (val < min) {
			return min;
		}
		if (val > max) {
			return max;
		}
		return val;
	}
}






	

public class NNet {
	private int inputAmount;
	private int outputAmount;

	List<float> inputs = new List<float>();
	NLayer inputlayer = new NLayer();

	List<NLayer> hiddenLayers = new List<NLayer>();
	NLayer outputLayer = new NLayer();

	List<float> outputs = new List<float> ();


	public void refresh(){
		outputs.Clear ();

		for (int i=0; i < hiddenLayers.Count; i++) {
			if(i > 0){
				inputs = outputs;
			}
			hiddenLayers[i].Evaluate(inputs, ref outputs);

		}
		inputs = outputs;
		//Process the layeroutputs through the output layer to
		outputLayer.Evaluate (inputs, ref outputs);

	}

	public void SetInput(List<float> input){
		inputs = input;
	}

	public float GetOutput(int ID){
		if (ID >= outputAmount)
			return 0.0f;
		return outputs [ID];
	}

	public int GetTotalOutputs() {
		return outputAmount;
	}

	public void CreateNet(int numOfHIddenLayers, int numOfInputs, int NeuronsPerHidden, int numOfOutputs){
		inputAmount = numOfInputs;
		outputAmount = numOfOutputs;

		for(int i=0; i<numOfHIddenLayers; i++){
			NLayer layer = new NLayer();
			layer.PopulateLayer(NeuronsPerHidden, numOfInputs);
			hiddenLayers.Add (layer);
		}

		outputLayer = new NLayer ();
		outputLayer.PopulateLayer (numOfOutputs, NeuronsPerHidden);
	}

	public void ReleaseNet(){
		if (inputlayer != null) {
			inputlayer = null;
			inputlayer = new NLayer();
		}
		if (outputLayer != null) {
			outputLayer = null;
			outputLayer = new NLayer();
		}
		for (int i=0; i<hiddenLayers.Count; i++) {
			if(hiddenLayers[i]!=null){
				hiddenLayers[i] = null;
			}
		}
		hiddenLayers.Clear ();
		hiddenLayers = new List<NLayer> ();
	}

	public int GetNumofHIddenLayers(){
		return hiddenLayers.Count;
	}

	public Genome ToGenome(){
		Genome genome = new Genome ();

		for (int i=0; i<this.hiddenLayers.Count; i++) {
			List<float> weights = new List<float> ();
			hiddenLayers[i].GetWeights(ref weights);
			for(int j=0; j<weights.Count;j++){
				genome.weights.Add (weights[j]);
			}
		}

		List<float> outweights = new List<float> ();
		outputLayer.GetWeights(ref outweights);
		for (int i=0; i<outweights.Count; i++) {
			genome.weights.Add (outweights[i]);
		}

		return genome;
	}

	public void FromGenome(Genome genome, int numofInputs, int neuronsPerHidden, int numOfOutputs){
		ReleaseNet ();

		outputAmount = numOfOutputs;
		inputAmount = numofInputs;

		int weightsForHidden = numofInputs * neuronsPerHidden;
		NLayer hidden = new NLayer ();

		List<Neuron> neurons = new List<Neuron>();

		for(int i=0; i<neuronsPerHidden; i++){
			//init
			neurons.Add(new Neuron());
			List<float> weights = new List<float>();
			//init

			for(int j=0; j<numofInputs+1;j++){
				weights.Add(0.0f);
				weights[j] = genome.weights[i*neuronsPerHidden + j];
			}
			neurons[i].weights = new List<float>();
			neurons[i].Initilise(weights, numofInputs);
		}
		hidden.LoadLayer (neurons);
		//Debug.Log ("fromgenome, hiddenlayer neruons#: " + neurons.Count);
		//Debug.Log ("fromgenome, hiddenlayer numInput: " + neurons [0].numInputs);
		this.hiddenLayers.Add (hidden);

		//Clear weights and reasign the weights to the output
		int weightsForOutput = neuronsPerHidden * numOfOutputs;
		List<Neuron> outneurons = new List<Neuron> ();

		for(int i=0; i<numOfOutputs; i++){
			outneurons.Add(new Neuron());

			List<float> weights = new List<float>();

			for(int j=0; j<neuronsPerHidden + 1; j++){
				weights.Add (0.0f);
				weights[j] = genome.weights[i*neuronsPerHidden + j];
			}
			outneurons[i].weights = new List<float>();
			outneurons[i].Initilise(weights, neuronsPerHidden);
		}
		this.outputLayer = new NLayer ();
		this.outputLayer.LoadLayer (outneurons);
		//Debug.Log ("fromgenome, outputlayer neruons#: " + outneurons.Count);
		//Debug.Log ("fromgenome, outputlayer numInput: " + outneurons [0].numInputs);
	}
}
public class NLayer {

	private int totalNeurons;
	private int totalInputs;


	List<Neuron> neurons = new List<Neuron>();

	public float Sigmoid(float a, float p) {
		float ap = (-a) / p;
		return (1 / (1 + Mathf.Exp (ap)));
	}

	public float BiPolarSigmoid(float a, float p){
		float ap = (-a) / p;
		return (2 / (1 + Mathf.Exp (ap)) - 1);
	}

	public void Evaluate(List<float> input, ref List<float> output){
		int inputIndex = 0;
		//Debug.Log ("input.count " + input.Count);
		//Debug.Log ("totalneuron " + totalNeurons);
		//cycle over all the neurons and sum their weights against the inputs
		for (int i=0; i< totalNeurons; i++) {
			float activation = 0.0f;

			//Debug.Log ("numInputs " + (neurons[i].numInputs - 1));

			//sum the weights to the activation value
			//we do the sizeof the weights - 1 so that we can add in the bias to the activation afterwards.
			for(int j=0; j< neurons[i].numInputs - 1; j++){

				activation += input[inputIndex] * neurons[i].weights[j];
				inputIndex++;
			}

			//add the bias
			//the bias will act as a threshold value to
			activation += neurons[i].weights[neurons[i].numInputs] * (-1.0f);//BIAS == -1.0f

			output.Add(Sigmoid(activation, 1.0f));
			inputIndex = 0;
		}
	}

	public void LoadLayer(List<Neuron> input){
		totalNeurons = input.Count;
		neurons = input;
	}

	public void PopulateLayer(int numOfNeurons, int numOfInputs){
		totalInputs = numOfInputs;
		totalNeurons = numOfNeurons;

		if (neurons.Count < numOfNeurons) {
			for(int i=0; i<numOfNeurons; i++){
				neurons.Add(new Neuron());
			}
		}

		for(int i=0; i<numOfNeurons; i++){
			neurons[i].Populate(numOfInputs);
		}
	}

	public void SetWeights(List<float> weights, int numOfNeurons, int numOfInputs){
		int index = 0;
		totalInputs = numOfInputs;
		totalNeurons = numOfNeurons;

		if (neurons.Count < numOfNeurons) {
			for (int i=0; i<numOfNeurons - neurons.Count; i++){
				neurons.Add(new Neuron());
			}
		}
		//Copy the weights into the neurons.
		for (int i=0; i<numOfNeurons; i++) {
			if(neurons[i].weights.Count < numOfInputs){
				for(int k=0; k<numOfInputs-neurons[i].weights.Count; k++){
					neurons[i].weights.Add (0.0f);
				}
			}
			for(int j=0; j<numOfInputs; j++){
				neurons[i].weights[j] = weights[index];
				index++;
			}
		}
	}

	public void GetWeights(ref List<float> output){
		//Calculate the size of the output list by calculating the amount of weights in each neurons.
		output.Clear ();

		for (int i=0; i<this.totalNeurons; i++) {
			for(int j=0; j<neurons[i].weights.Count; j++){
				output[totalNeurons*i + j] = neurons[i].weights[j];
			}
		}
	}

	public void SetNeurons(List<Neuron> neurons, int numOfNeurons, int numOfInputs){
		totalInputs = numOfInputs;
		totalNeurons = numOfNeurons;
		this.neurons = neurons;
	}
}


//=============================================================
public class Neuron {
	public int numInputs;
	public List<float> weights = new List<float>();


	public float RandomFloat()
	{
		float rand = (float)Random.Range (0.0f, 32767.0f);
		return rand / 32767.0f/*32767*/ + 1.0f;
	}

	public float RandomClamped()
	{
		return RandomFloat() - RandomFloat();
	}

	public float Clamp (float val, float min, float max){
		if (val < min) {
			return min;
		}
		if (val > max) {
			return max;
		}
		return val;
	}

	public void Populate(int num){
		this.numInputs = num;

		//Initilise the weights
		for (int i=0; i < num; i++){
			weights.Add(RandomClamped());
		}

		//add an extra weight as the bias (the value that acts as a threshold in a step activation).
		weights.Add (RandomClamped ());
	}

	public void Initilise(List<float> weightsIn, int num){
		this.numInputs = num;
		weights = weightsIn;
	}
}

//===================================
public class Genome{
	public float fitness;
	public int ID;
	public List<float> weights;

}