using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class DecodedNode {

    private string adultMovie;
    private string bellongsToCollection;
    private string budget;
    private List<string> genres;
    private string homepage;
    private string id;
    private string imdbId;
    private string originalLanguage;
    private string originalTitle;
    private string overview;
    private string popularity;
    private string posterPath;
    private List<string> productionCompanies;
    private List<string> productionCountries;
    private string releaseDate;
    private string revenue;
    private string runtime;
    private List<string> spokenLanguages;
    private string status;
    private string tagline;
    private string title;
    private string video;
    private string voteAverage;
    private string voteCount;

    public string PrintMovieInfo() {
        return 
            getAdultMovie() + "; " + 
            getBellongsToCollection() + "; " + 
            getBudget() + "; " + 
            getGenres() + "; " + 
            getHomepage()  + "; " + 
            getId() + "; " + 
            getImdbId() + "; " + 
            getOriginalLanguage() + "; " + 
            getOriginalTitle() + "; " + 
            getOverview() + "; " + 
            getPopularity() + "; " + 
            getPosterPath() + "; " + 
            getProductionCompanies() + "; " + 
            getProductionCountries() + "; " + 
            getReleaseDate() + "; " + 
            getRevenue() + "; " + 
            getRuntime() + "; " + 
            getSpokenLanguages() + "; " + 
            getStatus() + "; " + 
            getTagline() + "; " + 
            getTitle() + "; " + 
            getVideo() + "; " + 
            getVoteAverage() + "; " + 
            getVoteCount();
    }

    public string getAdultMovie()
    {
        return this.adultMovie;
    }

    public void setAdultMovie(string adultMovie)
    {
        this.adultMovie = adultMovie;
    }

    public string getBellongsToCollection()
    {
        return this.bellongsToCollection;
    }

    public void setBellongsToCollection(string bellongsToCollection)
    {
        if (bellongsToCollection.Length > 0 && bellongsToCollection.Trim()[0].ToString() == "{") {
            string[] values = bellongsToCollection.Split(","[0]);
            int nameIndex = bellongsToCollection.IndexOf("'name'");
            this.bellongsToCollection = values[nameIndex + 1].Replace("'", "");
        }
    }

    public string getBudget()
    {
        return this.budget;
    }

    public void setBudget(string budget)
    {
        this.budget = budget;
    }

    public List<string> getGenres()
    {
        return this.genres;
    }

    public void setGenres(string genres)
    {
        if (genres.Length > 0 && genres.Trim()[0].ToString() == "[" && genres.Trim()[1].ToString() == "{") {
            List<string> movieGenres = new List<string>(); 
            string[] values = genres.Split(","[0]);
            for (int i = 0; i < values.Length; i++) {
                if (values[i].Contains("name")) {
                    string[] aux = values[i].Split(":"[0]);
                    string genre = aux[aux.Length - 1].Trim();

                    // Accept only alphanumeric values
                    Regex rgx = new Regex("[^a-zA-Z0-9 -]"); 
                    movieGenres.Add(rgx.Replace(genre, ""));
                }
            }
            this.genres = movieGenres;
        }
    }

    public string getHomepage()
    {
        return this.homepage;
    }

    public void setHomepage(string homepage)
    {
        this.homepage = homepage;
    }

    public string getId()
    {
        return this.id;
    }

    public void setId(string id)
    {
        this.id = id;
    }

    public string getImdbId()
    {
        return this.imdbId;
    }

    public void setImdbId(string imdbId)
    {
        this.imdbId = imdbId;
    }

    public string getOriginalLanguage()
    {
        return this.originalLanguage;
    }

    public void setOriginalLanguage(string originalLanguage)
    {
        this.originalLanguage = originalLanguage;
    }

    public string getOriginalTitle()
    {
        return this.originalTitle;
    }

    public void setOriginalTitle(string originalTitle)
    {
        this.originalTitle = originalTitle;
    }

    public string getOverview()
    {
        return this.overview;
    }

    public void setOverview(string overview)
    {
        this.overview = overview;
    }

    public string getPopularity()
    {
        return this.popularity;
    }

    public void setPopularity(string popularity)
    {
        this.popularity = popularity;
    }

    public string getPosterPath()
    {
        return this.posterPath;
    }

    public void setPosterPath(string posterPath)
    {
        this.posterPath = posterPath;
    }

    public List<string> getProductionCompanies()
    {
        return this.productionCompanies;
    }

    public void setProductionCompanies(string productionCompanies)
    {
        if (productionCompanies.Length > 0 && productionCompanies.Trim()[0].ToString() == "[" && productionCompanies.Trim()[1].ToString() == "{") {
            List<string> companies = new List<string>(); 
            string[] values = productionCompanies.Split(","[0]);
            for (int i = 0; i < values.Length; i++) {
                if (values[i].Contains("name")) {
                    string[] aux = values[i].Split(":"[0]);
                    string company = aux[aux.Length - 1].Trim();

                    // Accept only alphanumeric values
                    Regex rgx = new Regex("[^a-zA-Z0-9 -]"); 
                    companies.Add(rgx.Replace(company, ""));
                }
            }
            this.productionCompanies = companies;
        }
    }

    public List<string> getProductionCountries()
    {
        return this.productionCountries;
    }

    public void setProductionCountries(string productionCountries)
    {
        if (productionCountries.Length > 0 && productionCountries.Trim()[0].ToString() == "[" && productionCountries.Trim()[1].ToString() == "{") {
            List<string> countries = new List<string>(); 
            string[] values = productionCountries.Split(","[0]);
            for (int i = 0; i < values.Length; i++) {
                if (values[i].Contains("name")) {
                    string[] aux = values[i].Split(":"[0]);
                    string country = aux[aux.Length - 1].Trim();

                    // Accept only alphanumeric values
                    Regex rgx = new Regex("[^a-zA-Z0-9 -]"); 
                    countries.Add(rgx.Replace(country, ""));
                }
            }
            this.productionCountries = countries;
        }
    }

    public string getReleaseDate()
    {
        return this.releaseDate;
    }

    public void setReleaseDate(string releaseDate)
    {
        this.releaseDate = releaseDate;
    }

    public string getRevenue()
    {
        return this.revenue;
    }

    public void setRevenue(string revenue)
    {
        this.revenue = revenue;
    }

    public string getRuntime()
    {
        return this.runtime;
    }

    public void setRuntime(string runtime)
    {
        this.runtime = runtime;
    }

    public List<string> getSpokenLanguages()
    {
        return this.spokenLanguages;
    }

    public void setSpokenLanguages(string spokenLanguages)
    {
        if (spokenLanguages.Length > 0 && spokenLanguages.Trim()[0].ToString() == "[" && spokenLanguages.Trim()[1].ToString() == "{") {
            List<string> languages = new List<string>(); 
            string[] values = spokenLanguages.Split(","[0]);
            for (int i = 0; i < values.Length; i++) {
                if (values[i].Contains("name")) {
                    string[] aux = values[i].Split(":"[0]);
                    string language = aux[aux.Length - 1].Trim();

                    // Accept only alphanumeric values
                    Regex rgx = new Regex("[^a-zA-Z0-9 -]"); 
                    languages.Add(rgx.Replace(language, ""));
                }
            }
            this.spokenLanguages = languages;
        }
    }

    public string getStatus()
    {
        return this.status;
    }

    public void setStatus(string status)
    {
        this.status = status;
    }

    public string getTagline()
    {
        return this.tagline;
    }

    public void setTagline(string tagline)
    {
        this.tagline = tagline;
    }

    public string getTitle()
    {
        return this.title;
    }

    public void setTitle(string title)
    {
        this.title = title;
    }

    public string getVideo()
    {
        return this.video;
    }

    public void setVideo(string video)
    {
        this.video = video;
    }

    public string getVoteAverage()
    {
        return this.voteAverage;
    }

    public void setVoteAverage(string voteAverage)
    {
        this.voteAverage = voteAverage;
    }

    public string getVoteCount()
    {
        return this.voteCount;
    }

    public void setVoteCount(string voteCount)
    {
        this.voteCount = voteCount;
    }






}
