import React, { SyntheticEvent } from "react";
import { CompanySearch } from "../../../company";
import CardPortfolio from "../CardPortfolio/CardPortfolio";

interface Props {
  portfolioValues: string[];
  OnRemovePortfolio: (e: SyntheticEvent) => void;
}

const ListPortfolio = ({ portfolioValues, OnRemovePortfolio }: Props) => {
  return (
    <>
      <h3>My Portfolio</h3>
      <ul>
        {portfolioValues &&
          portfolioValues.map((portfolioValue) => {
            return (
              <CardPortfolio
                portfolioValue={portfolioValue}
                OnRemovePortfolio={OnRemovePortfolio}
              />
            );
          })}
      </ul>
    </>
  );
};

export default ListPortfolio;
