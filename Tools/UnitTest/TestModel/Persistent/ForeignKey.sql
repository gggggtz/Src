--Model

ALTER TABLE [dbo].[Expression] WITH CHECK ADD CONSTRAINT [FK_dbo.Expression_dbo.ElementId] FOREIGN KEY([Id])
REFERENCES [dbo].[Element] ([Id]) 

ALTER TABLE [dbo].[Expression] CHECK CONSTRAINT [FK_dbo.Expression_dbo.ElementId]

ALTER TABLE [dbo].[IndexedFeature]  WITH CHECK ADD  CONSTRAINT [FK_dbo.IndexedFeature_dbo.ElementId] FOREIGN KEY([Id])
REFERENCES [dbo].[Element] ([Id]) 


ALTER TABLE [dbo].[IndexedFeature] CHECK CONSTRAINT [FK_dbo.IndexedFeature_dbo.ElementId]

ALTER TABLE [dbo].[KeyRelationshipFeature]  WITH CHECK ADD  CONSTRAINT [FK_dbo.KeyRelationshipFeature_dbo.Element_ElementId] FOREIGN KEY([Id])
REFERENCES [dbo].[Element] ([Id]) 

ALTER TABLE [dbo].[KeyRelationshipFeature] CHECK CONSTRAINT [FK_dbo.KeyRelationshipFeature_dbo.Element_ElementId]

ALTER TABLE [dbo].[ModelElement]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ModelElement_dbo.ElementId] FOREIGN KEY([Id])
REFERENCES [dbo].[Element] ([Id]) 

ALTER TABLE [dbo].[ModelElement] CHECK CONSTRAINT [FK_dbo.ModelElement_dbo.ElementId]

ALTER TABLE [dbo].[UniqueKeyFeature]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UniqueKeyFeatures_dbo.Element_ElementId] FOREIGN KEY([Id])
REFERENCES [dbo].[Element] ([Id]) 

ALTER TABLE [dbo].[UniqueKeyFeature] CHECK CONSTRAINT [FK_dbo.UniqueKeyFeatures_dbo.Element_ElementId]

ALTER TABLE [dbo].[Feature]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Feature_dbo.ModelElementId] FOREIGN KEY([Id])
REFERENCES [dbo].[ModelElement] ([Id]) 

ALTER TABLE [dbo].[Feature] CHECK CONSTRAINT [FK_dbo.Feature_dbo.ModelElementId]

ALTER TABLE [dbo].[Index]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Index_dbo.ModelElementId] FOREIGN KEY([Id])
REFERENCES [dbo].[ModelElement] ([Id]) 

ALTER TABLE [dbo].[Index] CHECK CONSTRAINT [FK_dbo.Index_dbo.ModelElementId]

ALTER TABLE [dbo].[KeyRelationship]  WITH CHECK ADD  CONSTRAINT [FK_dbo.KeyRelationship_dbo.ModelElementId] FOREIGN KEY([Id])
REFERENCES [dbo].[ModelElement] ([Id]) 

ALTER TABLE [dbo].[KeyRelationship] CHECK CONSTRAINT [FK_dbo.KeyRelationship_dbo.ModelElementId]

ALTER TABLE [dbo].[ModelElement]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ModelElement_dbo.NameSpace_OwnerId] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[NameSpace] ([Id])

ALTER TABLE [dbo].[ModelElement] CHECK CONSTRAINT [FK_dbo.ModelElement_dbo.NameSpace_OwnerId]

ALTER TABLE [dbo].[NameSpace]  WITH CHECK ADD  CONSTRAINT [FK_dbo.NameSpace_dbo.ModelElementId] FOREIGN KEY([Id])
REFERENCES [dbo].[ModelElement] ([Id]) 

ALTER TABLE [dbo].[NameSpace] CHECK CONSTRAINT [FK_dbo.NameSpace_dbo.ModelElementId]

ALTER TABLE [dbo].[UniqueKey]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UniqueKey_dbo.ModelElementId] FOREIGN KEY([Id])
REFERENCES [dbo].[ModelElement] ([Id]) 

ALTER TABLE [dbo].[UniqueKey] CHECK CONSTRAINT [FK_dbo.UniqueKey_dbo.ModelElementId]

ALTER TABLE [dbo].[Column]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Column_dbo.FeatureId] FOREIGN KEY([Id])
REFERENCES [dbo].[Feature] ([Id]) 

ALTER TABLE [dbo].[Column] CHECK CONSTRAINT [FK_dbo.Column_dbo.FeatureId]

ALTER TABLE [dbo].[Feature]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Feature_dbo.Classifier_ClassifierId] FOREIGN KEY([ClassifierId])
REFERENCES [dbo].[Classifier] ([Id]) 

ALTER TABLE [dbo].[Feature] CHECK CONSTRAINT [FK_dbo.Feature_dbo.Classifier_ClassifierId]

ALTER TABLE [dbo].[Feature]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Feature_dbo.Expression_InitialValueId] FOREIGN KEY([InitialValueId])
REFERENCES [dbo].[Expression] ([Id])

ALTER TABLE [dbo].[Feature] CHECK CONSTRAINT [FK_dbo.Feature_dbo.Expression_InitialValueId]

ALTER TABLE [dbo].[IndexedFeature]  WITH CHECK ADD  CONSTRAINT [FK_dbo.IndexedFeature_dbo.Feature_StructuralFeatureId] FOREIGN KEY([StructuralFeatureId])
REFERENCES [dbo].[Feature] ([Id]) 

ALTER TABLE [dbo].[IndexedFeature] CHECK CONSTRAINT [FK_dbo.IndexedFeature_dbo.Feature_StructuralFeatureId]

ALTER TABLE [dbo].[KeyRelationshipFeature]  WITH CHECK ADD  CONSTRAINT [FK_dbo.KeyRelationshipFeature_dbo.Feature_StructuralFeatureId] FOREIGN KEY([StructuralFeatureId])
REFERENCES [dbo].[Feature] ([Id]) 

ALTER TABLE [dbo].[KeyRelationshipFeature] CHECK CONSTRAINT [FK_dbo.KeyRelationshipFeature_dbo.Feature_StructuralFeatureId]

ALTER TABLE [dbo].[UniqueKeyFeature]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UniqueKeyFeatures_dbo.Feature_StructuralFeatureId] FOREIGN KEY([StructuralFeatureId])
REFERENCES [dbo].[Feature] ([Id])

ALTER TABLE [dbo].[UniqueKeyFeature] CHECK CONSTRAINT [FK_dbo.UniqueKeyFeatures_dbo.Feature_StructuralFeatureId]

ALTER TABLE [dbo].[Classifier]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Classifier_dbo.NameSpaceId] FOREIGN KEY([Id])
REFERENCES [dbo].[NameSpace] ([Id]) 

ALTER TABLE [dbo].[Classifier] CHECK CONSTRAINT [FK_dbo.Classifier_dbo.NameSpaceId]

ALTER TABLE [dbo].[Database]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Database_dbo.NameSpaceId] FOREIGN KEY([Id])
REFERENCES [dbo].[NameSpace] ([Id]) 

ALTER TABLE [dbo].[Database] CHECK CONSTRAINT [FK_dbo.Database_dbo.NameSpaceId]


ALTER TABLE [dbo].[Schema]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Schema_dbo.NameSpaceId] FOREIGN KEY([Id])
REFERENCES [dbo].[NameSpace] ([Id]) 

ALTER TABLE [dbo].[Schema] CHECK CONSTRAINT [FK_dbo.Schema_dbo.NameSpaceId]

ALTER TABLE [dbo].[Table]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Table_dbo.ClassifierId] FOREIGN KEY([Id])
REFERENCES [dbo].[Classifier] ([Id]) 

ALTER TABLE [dbo].[Table] CHECK CONSTRAINT [FK_dbo.Table_dbo.ClassifierId]

ALTER TABLE [dbo].[IndexedFeature]  WITH CHECK ADD  CONSTRAINT [FK_dbo.IndexedFeature_dbo.Index_IndexId] FOREIGN KEY([IndexId])
REFERENCES [dbo].[Index] ([Id]) 

ALTER TABLE [dbo].[IndexedFeature] CHECK CONSTRAINT [FK_dbo.IndexedFeature_dbo.Index_IndexId]

ALTER TABLE [dbo].[KeyRelationship]  WITH CHECK ADD  CONSTRAINT [FK_dbo.KeyRelationship_dbo.UniqueKey_UniqueKeyId] FOREIGN KEY([UniqueKeyId])
REFERENCES [dbo].[UniqueKey] ([Id]) 

ALTER TABLE [dbo].[KeyRelationship] CHECK CONSTRAINT [FK_dbo.KeyRelationship_dbo.UniqueKey_UniqueKeyId]

ALTER TABLE [dbo].[KeyRelationshipFeature]  WITH CHECK ADD  CONSTRAINT [FK_dbo.KeyRelationshipFeature_dbo.KeyRelationship_KeyRelationshipId] FOREIGN KEY([KeyRelationshipId])
REFERENCES [dbo].[KeyRelationship] ([Id]) 

ALTER TABLE [dbo].[KeyRelationshipFeature] CHECK CONSTRAINT [FK_dbo.KeyRelationshipFeature_dbo.KeyRelationship_KeyRelationshipId]

ALTER TABLE [dbo].[UniqueKeyFeature]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UniqueKeyFeatures_dbo.UniqueKey_UniqueKeyId] FOREIGN KEY([UniqueKeyId])
REFERENCES [dbo].[UniqueKey] ([Id])

ALTER TABLE [dbo].[UniqueKeyFeature] CHECK CONSTRAINT [FK_dbo.UniqueKeyFeatures_dbo.UniqueKey_UniqueKeyId]


