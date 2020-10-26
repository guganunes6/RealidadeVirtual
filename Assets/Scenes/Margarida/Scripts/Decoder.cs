using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class Decoder {

    private string dirPath;

    public Decoder(string dirPath) {
        this.dirPath = dirPath;
    }

    public void Parse(int numNodesToParse) {
        using(var reader = new StreamReader(this.dirPath)) {
            Dictionary<string, DecodedNode> movies = new Dictionary<string, DecodedNode>(); // {id : DecodedNode}

            do { 
                string line = reader.ReadLine();
                string[] columns = line.Split(","[0]);

                List<string> columnsList = ParseLine(columns);
                if (columnsList.Count == 24) { // Total good columns
                    DecodedNode node = CreateNode(columnsList);
                    string id = node.getId(); 
                    if (!movies.ContainsKey(id) && id != "id") movies.Add(id, node); // exclude the first line
                }
            } while (movies.Count < numNodesToParse && !reader.EndOfStream);
            
            // Nodes info {id : DecodedNode} => DecodedNode has 24 different parameters about the movie
            foreach (KeyValuePair<string, DecodedNode> kv in movies) {
                Debug.Log ("\n{" + kv.Key.ToString() +  " : (" + kv.Value.PrintMovieInfo() + ")}");
            }
        }
    }

    private List<string> ParseLine(string[] columns) {
        List<string> values = new List<string>();

        // Loop all dataset columns info
        for (int i = 0; i < columns.Length; i++) { 
            
            // If first char is " => need to join elements
            if (columns[i].Trim() != "" && columns[i].Trim()[0].ToString() == "\"") { 
                Dictionary<int, string> result = AppendUntilJsonCompleted(columns, i);
                values.Add(result.First().Value);
                i = result.First().Key;
            } else if (columns[i] == "") {
                values.Add("");
            } else {
                values.Add(columns[i]);
            }
        } 
        return values;
    }

    private Dictionary<int, string> AppendUntilJsonCompleted(string[] columns, int startAt) {
        Dictionary<int, string> result = new Dictionary<int, string>();
        string component = "";
        int finishAt = startAt; 
        bool toAdd = true;

        // while we still inside JSON we keep adding info
        while (columns[finishAt] != "" && columns[finishAt][columns[finishAt].Length - 1].ToString() != "\"") { 
            component += columns[finishAt];
            finishAt += 1;
            if (finishAt >= columns.Length) {
                Debug.Log("ALERT IndexOutOfBounds ERROR!");
                toAdd = false;
                break;
            }
        }

        // add last one too, is the last with "
        if (toAdd) component += columns[finishAt]; 
        result.Add(finishAt, component);
        return result;
    }

    private DecodedNode CreateNode(List<string> columnsList) {
        DecodedNode node = new DecodedNode();
        node.setAdultMovie(columnsList[0]);
        node.setBellongsToCollection(columnsList[1]);
        node.setBudget(columnsList[2]);
        node.setGenres(columnsList[3]);
        node.setHomepage(columnsList[4]);
        node.setId(columnsList[5]);
        node.setImdbId(columnsList[6]);
        node.setOriginalLanguage(columnsList[7]);
        node.setOriginalTitle(columnsList[8]);
        node.setOverview(columnsList[9]);
        node.setPopularity(columnsList[10]);
        node.setPosterPath(columnsList[11]);
        node.setProductionCompanies(columnsList[12]);
        node.setProductionCountries(columnsList[13]);
        node.setReleaseDate(columnsList[14]);
        node.setRevenue(columnsList[15]);
        node.setRuntime(columnsList[16]);
        node.setSpokenLanguages(columnsList[17]);
        node.setStatus(columnsList[18]);
        node.setTagline(columnsList[19]);
        node.setTitle(columnsList[20]);
        node.setVideo(columnsList[21]);
        node.setVoteAverage(columnsList[22]);
        node.setVoteCount(columnsList[23]);
        return node;
    }
}
