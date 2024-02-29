#!/usr/bin/env python
# coding: utf-8

# In[1]:


import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns


# In[2]:


data = pd.read_csv(r"file_path")


# In[3]:


# Convert DateOfAdmission and DateOfDischarge to datetime
data['DateOfAdmission'] = pd.to_datetime(data['DateOfAdmission'])
data['DateOfDischarge'] = pd.to_datetime(data['DateOfDischarge'])

# Calculate Length of Stay for each record
data['LengthOfStay'] = (data['DateOfDischarge'] - data['DateOfAdmission']).dt.days


# In[4]:


# Group data by DiagnosisCode and TreatmentType
treatment_effectiveness = data.groupby(['DiagnosisCode', 'TreatmentType']).agg({
    'CostOfTreatment': 'mean',
    'LengthOfStay': 'mean'
}).reset_index()


# In[5]:


# Set the figure size and style for better visibility
plt.figure(figsize=(14, 8))
sns.set_style("whitegrid")

# Plotting
sns.scatterplot(data=treatment_effectiveness, x='LengthOfStay', y='CostOfTreatment', hue='TreatmentType', style='DiagnosisCode', s=100)

# Enhancing the plot
plt.title('Treatment Effectiveness: Cost vs. Length of Stay by Diagnosis and Treatment Type')
plt.xlabel('Average Length of Stay (Days)')
plt.ylabel('Average Cost of Treatment')
plt.legend(title='Treatment Type', bbox_to_anchor=(1.05, 1), loc='upper left')

# Show plot
plt.tight_layout()
plt.show()


# In[6]:


# Group data by Age, Gender, and TreatmentType
patient_outcomes = data.groupby(['Age', 'Gender', 'TreatmentType']).agg({
    'CostOfTreatment': 'mean',
    'LengthOfStay': 'mean'
}).reset_index()


# In[7]:


plt.figure(figsize=(14, 7))
sns.scatterplot(data=patient_outcomes, x='Age', y='CostOfTreatment', hue='Gender', style='TreatmentType', s=100)
plt.title('Cost of Treatment by Age and Gender')
plt.xlabel('Age')
plt.ylabel('Average Cost of Treatment')
plt.legend(title='Gender', bbox_to_anchor=(1.05, 1), loc='upper left')
plt.grid(True)
plt.show()


# In[8]:


plt.figure(figsize=(14, 7))
sns.scatterplot(data=patient_outcomes, x='Age', y='LengthOfStay', hue='Gender', style='TreatmentType', s=100)
plt.title('Length of Stay by Age and Gender')
plt.xlabel('Age')
plt.ylabel('Average Length of Stay (Days)')
plt.legend(title='Gender', bbox_to_anchor=(1.05, 1), loc='upper left')
plt.grid(True)
plt.show()


# In[9]:


plt.figure(figsize=(10, 6))
sns.violinplot(data=data, x='Gender', y='LengthOfStay', palette='Set3', inner='quartile')
plt.title('Distribution of Length of Stay by Gender')
plt.xlabel('Gender')
plt.ylabel('Length of Stay (Days)')
plt.show()


# In[10]:


# Group data by DiagnosisCode and calculate the average cost
avg_cost_by_diagnosis = data.groupby('DiagnosisCode')['CostOfTreatment'].mean().reset_index()

# Sort the data for better visualization
avg_cost_by_diagnosis = avg_cost_by_diagnosis.sort_values(by='CostOfTreatment', ascending=False)


# In[11]:


# Group data by TreatmentType and calculate the average cost
avg_cost_by_treatment = data.groupby('TreatmentType')['CostOfTreatment'].mean().reset_index()

# Sort the data for better visualization
avg_cost_by_treatment = avg_cost_by_treatment.sort_values(by='CostOfTreatment', ascending=False)


# In[12]:


# Plotting Average Cost of Treatment by Diagnosis Code
plt.figure(figsize=(12, 6))
sns.barplot(data=avg_cost_by_diagnosis, x='DiagnosisCode', y='CostOfTreatment', palette='coolwarm')
plt.xticks(rotation=45)
plt.title('Average Cost of Treatment by Diagnosis Code')
plt.xlabel('Diagnosis Code')
plt.ylabel('Average Cost of Treatment')
plt.show()

# Plotting Average Cost of Treatment by Treatment Type
plt.figure(figsize=(10, 6))
sns.barplot(data=avg_cost_by_treatment, x='TreatmentType', y='CostOfTreatment', palette='viridis')
plt.title('Average Cost of Treatment by Treatment Type')
plt.xlabel('Treatment Type')
plt.ylabel('Average Cost of Treatment')
plt.show()


# In[ ]:




