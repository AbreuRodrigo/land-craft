using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HashManager {
	const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
	const string PLUS = "z1x2c3a4s5d6q7w8e9";
	const int HASH_LENGTH = 10;

	public Dictionary<string, int> hashedNumbers = new Dictionary<string, int>();
	private Dictionary<int, string> numbersToHash = new Dictionary<int, string>();

	private int seed;
	private int amountHashes;
	private int returningHashInt;
	private string returningHashValue;

	public HashManager(int hashes, string uuid) {
		amountHashes = hashes;

		if(uuid == null) {
			uuid = CHARS;
		}

		uuid += PLUS;

		seed = (int)Mathf.Abs(uuid.GetHashCode());
		Random.seed = seed;

		GenerateHashes();
	}

	private void GenerateHashes() {
		char[] stringChars = new char[HASH_LENGTH];
		string serial = "";

		for(int j = 0; j < amountHashes; j++) {
			for(int i = 0; i < HASH_LENGTH; i++) {
				serial += CHARS[Randomize(0, CHARS.Length)];
			}

			hashedNumbers[serial] = j + 1;
			numbersToHash[j + 1] = serial;

			serial = "";
		}
	}

	public int HashToInt(string hashKey) {
		if(hashedNumbers.TryGetValue(hashKey, out returningHashInt)) {
			return returningHashInt;
		}

		return -1;
	}

	public string IntToHash(int hashValue) {
		if(numbersToHash.TryGetValue(hashValue + 1, out returningHashValue)) {
			return returningHashValue;
		}

		return "";
	}

	public int Randomize(int from, int to) {
		return Random.Range(from, to);
	}
}