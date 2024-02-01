import React, { ChangeEvent, SyntheticEvent, useState } from "react";
import logo from "./logo.svg";
import "./App.css";
import Card from "./Components/Card/Card";
import CardList from "./Components/CardList/CardList";
import Search from "./Components/Search/Search";
import { CompanySearch } from "./company";
import { searchCompanies } from "./api";
import ListPortfolio from "./Components/Portfolio/ListPortfolio/ListPortfolio";

function App() {
  const [search, setSearch] = useState<string>("");
  const [portfolioValues, setPortfolioValues] = useState<string[]>([]);
  const [searchIndex, setSearchIndex] = useState<CompanySearch[]>([]);
  const [serverError, setServerError] = useState<string>("");

  const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
    //console.log(e);
  };

  const onSearchSubmit = async (e: SyntheticEvent) => {
    e.preventDefault();
    const result = await searchCompanies(search);
    if (typeof result == "string") {
      setServerError(result);
    } else if (result?.data == null) {
      setServerError("");
    } else if (Array.isArray(result.data)) {
      setSearchIndex(result.data);
    }

    console.log(searchIndex);
  };

  const OnSubmitPortfolio = (e: any) => {
    e.preventDefault();
    const exists = portfolioValues.find((value) => value === e.target[0].value);
    if (exists) return;
    const updatedPortfolio = [...portfolioValues, e.target[0].value];
    setPortfolioValues(updatedPortfolio);
    //console.log(e);
  };

  const OnRemovePortfolio = (e: any) => {
    e.preventDefault();
    const removed = portfolioValues.filter((value) => {
      return value !== e.target[0].value;
    });

    setPortfolioValues(removed);
  };

  return (
    <div className="App">
      <Search
        onSearchSubmit={onSearchSubmit}
        search={search}
        handleSearchChange={handleSearchChange}
      />
      <ListPortfolio
        portfolioValues={portfolioValues}
        OnRemovePortfolio={OnRemovePortfolio}
      />
      <CardList
        searchResults={searchIndex}
        OnSubmitPortfolio={OnSubmitPortfolio}
      />
      {{ serverError } && <h1>{serverError}</h1>}
    </div>
  );
}

export default App;
