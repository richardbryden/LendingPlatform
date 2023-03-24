using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LendingPlatform.Objects;

namespace LendingPlatform
{
    public class Platform
    {
        public static void Run()
        {
            bool endApp = false;
            Console.WriteLine("Lending Platform\r");
            //global variables used for tracking stats
            Stats stats = new Stats();
            while (!endApp)
            {
                Inputs inputs = RequestInputs();

                stats.applications++;

                // Check for application status
                bool applicationStatus = CheckApplication(inputs.loanAmountInput, inputs.creditScore, inputs.ltv);

                // Assumes only loans that are accepted are added to the loan total
                if (applicationStatus && inputs.loanAmountInput != null)
                {
                    stats.succesfulApplications++;
                    stats.loanTotal += inputs.loanAmountInput;
                }
                else
                {
                    stats.unsuccesfulApplications++;
                }

                // calculating average LTV of all loan requests
                stats.averageLTV = (stats.averageLTV * (stats.applications - 1) + inputs.ltv) / stats.applications;

                Output(stats, applicationStatus);
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n");
            }
        }

        public static bool CheckApplication(double? loanAmountInput, int? creditScore, double? ltv)
        {
            //reject any loans below 100k and over 1.5m
            if (loanAmountInput > 1500000 || loanAmountInput < 100000)
            {
                return false;
            }
            else
            {
                //handling loans over 1m
                if (loanAmountInput >= 1000000)
                {
                    if (ltv >= 0.6 || creditScore < 950)
                    {
                        return false;
                    }
                }
                // Checking loans between 1m and 100k
                else
                {
                    if (ltv >= 0.9 ||
                        (ltv < 0.9 && ltv >= 0.8 && creditScore < 900) ||
                        (ltv < 0.8 && ltv >= 0.6 && creditScore < 800) ||
                        (ltv < 0.6 && creditScore < 750))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static Inputs RequestInputs()
        {
            Inputs inputs = new Inputs();

            //input request, loops till inputs are in correct format, improvment would be to allow individual to exit part way through a loan request
            while (inputs.loanAmountInput == null)
            {
                double k = new double();
                Console.WriteLine("Please input loan amount in £");
                if (double.TryParse(Console.ReadLine(), out k))
                {
                    inputs.loanAmountInput = k;
                };
            }

            while (inputs.assetValue == null)
            {
                double k = new double();
                Console.WriteLine("Please input the Asset value");
                if (double.TryParse(Console.ReadLine(), out k))
                {
                    inputs.assetValue = k;
                };
            }

            while (inputs.creditScore == null)
            {
                int k = new int();
                Console.WriteLine("Please input the credit score");
                if (int.TryParse(Console.ReadLine(), out k))
                {
                    inputs.creditScore = k;
                };
            }

            while (inputs.creditScore < 1 || inputs.creditScore > 999)
            {
                int k = new int();
                Console.WriteLine("Credit score invalid, please input a vaild credit score");
                if (int.TryParse(Console.ReadLine(), out k))
                {
                    inputs.creditScore = k;
                };
            }

            inputs.ltv = inputs.loanAmountInput != null && inputs.assetValue != null ? (double)(inputs.loanAmountInput / inputs.assetValue) : 0;

            return inputs;
        }

        public static void Output(Stats stats, bool applicationStatus)
        {
            Console.WriteLine(applicationStatus ? "Application Accepted" : "Application Declined");
            Console.WriteLine("Applications: " + (stats.applications).ToString());
            Console.WriteLine("Successful applications: " + stats.succesfulApplications.ToString());
            Console.WriteLine("Unsuccessful applications: " + stats.unsuccesfulApplications.ToString());
            Console.WriteLine("Value of loans written " + stats.loanTotal.ToString());
            Console.WriteLine("Average LTV of all applications " + stats.averageLTV.ToString());
        }
    }
}
